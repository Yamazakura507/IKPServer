using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using IKP.Application.Common.Query;
using IKP.Application.Common.Query.Enums;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Infrastructure.Query;

/// <summary>
/// Indicates that a universal query is invalid for the current resource or configuration.
/// </summary>
public sealed class UniversalQueryException : Exception
{
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    public UniversalQueryException(string message) : base(message)
    {
    }
}

/// <summary>
/// Provides reusable EF Core query construction for API lists and grouped aggregates.
/// </summary>
public static class UniversalQueryExtensions
{
    private const int MaximumAggregateCount = 8;

    /// <summary>
    /// Applies filters, search, sorting and pagination using whitelisted fields.
    /// </summary>
    public static async Task<PagedResponse<TProjection>> ToPagedResponseAsync<TEntity, TProjection>(
        this IQueryable<TEntity> query,
        UniversalQueryRequest request,
        IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields,
        Expression<Func<TEntity, TProjection>> projection,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(fields);
        ArgumentNullException.ThrowIfNull(projection);

        ValidateRequest(request);

        if (request.Aggregation is not null) throw new UniversalQueryException("Aggregation requests must use a grouped aggregate endpoint.");

        query = ApplyFilter(query, request.Filter, fields);
        query = ApplySearch(query, request.Search, fields);
        query = ApplySort(query, request.Sorts, fields);

        long totalCount = await query.LongCountAsync(cancellationToken);
        int page = request.Page;
        int pageSize = request.PageSize;
        int totalPages = totalCount == 0 ? 0 : checked((int)Math.Ceiling(totalCount / (double)pageSize));

        List<TProjection> items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(projection).ToListAsync(cancellationToken);

        return new PagedResponse<TProjection>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    /// <summary>
    /// Executes a grouped aggregate query with a single grouping field and up to eight aggregates.
    /// </summary>
    public static async Task<UniversalAggregateResponse> ToAggregateResponseAsync<TEntity>(
        this IQueryable<TEntity> query,
        UniversalQueryRequest request,
        IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(fields);

        ValidateRequest(request);

        UniversalAggregationRequest aggregation = request.Aggregation ?? throw new UniversalQueryException("Aggregation is required.");

        if (string.IsNullOrWhiteSpace(aggregation.GroupBy)) throw new UniversalQueryException("Aggregation.GroupBy is required.");
        if (aggregation.Aggregates.Count == 0) throw new UniversalQueryException("At least one aggregate is required.");
        if (aggregation.Aggregates.Count > MaximumAggregateCount) throw new UniversalQueryException($"A maximum of {MaximumAggregateCount} aggregates is supported per request.");
        if (!fields.TryGetValue(aggregation.GroupBy, out Expression<Func<TEntity, object?>>? groupSelector)) throw new UniversalQueryException($"Group field '{aggregation.GroupBy}' is not available for this resource.");

        query = ApplyFilter(query, request.Filter, fields);
        query = ApplySearch(query, request.Search, fields);

        Type groupType = GetSelectorValueType(groupSelector);
        Type rowType = typeof(UniversalAggregateProjection<>).MakeGenericType(groupType);
        Type groupingType = typeof(IGrouping<,>).MakeGenericType(groupType, typeof(TEntity));
        ParameterExpression groupingParameter = Expression.Parameter(groupingType, "group");
        Expression groupKey = Expression.Property(groupingParameter, nameof(IGrouping<object, object>.Key));

        List<MemberBinding> bindings = [ Expression.Bind(rowType.GetProperty(nameof(UniversalAggregateProjection<int>.GroupValue))!, groupKey) ];

        for (int index = 0; index < aggregation.Aggregates.Count; index++)
        {
            UniversalAggregateRequest aggregate = aggregation.Aggregates[index];
            Expression aggregateExpression = BuildAggregateExpression(groupingParameter, aggregate, fields);
            bindings.Add(Expression.Bind(rowType.GetProperty($"Value{index + 1}")!, aggregateExpression));
        }

        MemberInitExpression projection = Expression.MemberInit(Expression.New(rowType), bindings);
        LambdaExpression projectionLambda = Expression.Lambda(projection, groupingParameter);
        MethodInfo groupByMethod = GetQueryableMethod(false).MakeGenericMethod(typeof(TEntity), groupType);
        MethodInfo selectMethod = GetQueryableMethod(true).MakeGenericMethod(groupingType, rowType);

        LambdaExpression typedGroupSelector = Expression.Lambda(GetSelectorBody(groupSelector.Body), groupSelector.Parameters);
        IQueryable groupedQuery = (IQueryable)groupByMethod.Invoke(null, [query, typedGroupSelector])!;
        IQueryable resultQuery = (IQueryable)selectMethod.Invoke(null, [groupedQuery, projectionLambda])!;
        int page = request.Page;
        int pageSize = request.PageSize;
        long totalCount = await resultQuery.Cast<object>().LongCountAsync(cancellationToken);
        List<object> rows = await resultQuery.Cast<object>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        List<string> aliases = aggregation.Aggregates.Select(x => x.Alias).ToList();
        List<UniversalAggregateRow> resultRows = rows.Select(row =>
        {
            object? groupValue = rowType.GetProperty(nameof(UniversalAggregateProjection<int>.GroupValue))!.GetValue(row);
            Dictionary<string, decimal?> values = new(StringComparer.OrdinalIgnoreCase);

            for (int index = 0; index < aliases.Count; index++)
            {
                values[aliases[index]] = (decimal?)rowType.GetProperty($"Value{index + 1}")!.GetValue(row);
            }

            return new UniversalAggregateRow { GroupValue = groupValue, Values = values };
        }).ToList();

        return new UniversalAggregateResponse
        {
            GroupBy = aggregation.GroupBy,
            AggregateAliases = aliases,
            Rows = resultRows
        };
    }

    private static MethodInfo GetQueryableMethod(bool isSelect) => typeof(Queryable).GetMethods()
        .Single(x =>
            x.Name == (isSelect ? nameof(Queryable.Select) : nameof(Queryable.GroupBy)) &&
            x.IsGenericMethodDefinition &&
            x.GetGenericArguments().Length == 2 &&
            x.GetParameters().Length == 2 &&
            x.GetParameters()[1].ParameterType.IsGenericType &&
            x.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>) &&
            x.GetParameters()[1].ParameterType.GetGenericArguments()[0].IsGenericType &&
            x.GetParameters()[1].ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Func<,>));

    private static Expression BuildAggregateExpression<TEntity>(ParameterExpression grouping, UniversalAggregateRequest aggregate, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        if (string.IsNullOrWhiteSpace(aggregate.Alias)) throw new UniversalQueryException("Every aggregate must have an alias.");
        Type entityType = typeof(TEntity);
        Type enumerableType = typeof(IEnumerable<>).MakeGenericType(entityType);
        Expression groupAsEnumerable = grouping;

        return aggregate.Function switch
        {
            UniversalAggregateFunction.Count => ConvertToNullableDecimal(Expression.Call(typeof(Enumerable), nameof(Enumerable.LongCount), [entityType], groupAsEnumerable)),
            UniversalAggregateFunction.CountDistinct => BuildCountDistinct(grouping, aggregate, fields, entityType),
            UniversalAggregateFunction.Sum => BuildNumericAggregate(grouping, aggregate, fields, entityType, nameof(Enumerable.Sum)),
            UniversalAggregateFunction.Average => BuildNumericAggregate(grouping, aggregate, fields, entityType, nameof(Enumerable.Average)),
            UniversalAggregateFunction.Minimum => BuildNumericAggregate(grouping, aggregate, fields, entityType, nameof(Enumerable.Min)),
            UniversalAggregateFunction.Maximum => BuildNumericAggregate(grouping, aggregate, fields, entityType, nameof(Enumerable.Max)),
            _ => throw new UniversalQueryException($"Unsupported aggregate function '{aggregate.Function}'.")
        };
    }

    private static Expression BuildCountDistinct<TEntity>(ParameterExpression grouping, UniversalAggregateRequest aggregate, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields, Type entityType)
    {
        Expression selector = GetRequiredSelector(aggregate, fields, out Type valueType);
        ParameterExpression item = Expression.Parameter(entityType, "item");
        Expression body = ReplaceParameter(GetSelectorBody(selector), fields[aggregate.Field!].Parameters[0], item);
        LambdaExpression lambda = Expression.Lambda(body, item);
        MethodInfo select = typeof(Enumerable).GetMethods().Single(x => x.Name == nameof(Enumerable.Select) && x.GetParameters().Length == 2).MakeGenericMethod(entityType, valueType);
        Expression selected = Expression.Call(select, grouping, lambda);
        MethodInfo distinct = typeof(Enumerable).GetMethods().Single(x => x.Name == nameof(Enumerable.Distinct) && x.GetParameters().Length == 1).MakeGenericMethod(valueType);
        Expression distinctValues = Expression.Call(distinct, selected);
        MethodInfo count = typeof(Enumerable).GetMethods().Single(x => x.Name == nameof(Enumerable.LongCount) && x.GetParameters().Length == 1).MakeGenericMethod(valueType);
        
        return ConvertToNullableDecimal(Expression.Call(count, distinctValues));
    }

    private static Expression BuildNumericAggregate<TEntity>(ParameterExpression grouping, UniversalAggregateRequest aggregate, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields, Type entityType, string methodName)
    {
        Expression selector = GetRequiredSelector(aggregate, fields, out _);
        ParameterExpression item = Expression.Parameter(entityType, "item");
        Expression body = ReplaceParameter(GetSelectorBody(selector), fields[aggregate.Field!].Parameters[0], item);
        Type sourceType = body.Type;
        Type numericType = Nullable.GetUnderlyingType(sourceType) ?? sourceType;

        if (!IsNumericType(numericType)) throw new UniversalQueryException($"Aggregate '{aggregate.Alias}' requires a numeric field.");

        Expression decimalBody = body.Type == typeof(decimal?) ? body : Expression.Convert(body, typeof(decimal?));
        LambdaExpression lambda = Expression.Lambda(decimalBody, item);
        int genericArgumentCount = methodName is nameof(Enumerable.Min) or nameof(Enumerable.Max) ? 2 : 1;

        MethodInfo method = typeof(Enumerable).GetMethods()
            .Where(x => x.Name == methodName && x.GetParameters().Length == 2 && x.GetGenericArguments().Length == genericArgumentCount)
            .Single(x => x.GetParameters()[1].ParameterType.GetGenericArguments().Length == 2)
            .MakeGenericMethod(genericArgumentCount == 1 ? [entityType] : [entityType, typeof(decimal?)]);

        return ConvertToNullableDecimal(Expression.Call(method, grouping, lambda));
    }

    private static Expression GetRequiredSelector<TEntity>(UniversalAggregateRequest aggregate, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields, out Type valueType)
    {
        if (string.IsNullOrWhiteSpace(aggregate.Field)) throw new UniversalQueryException($"Aggregate '{aggregate.Alias}' requires a field.");
        if (!fields.TryGetValue(aggregate.Field, out Expression<Func<TEntity, object?>>? selector)) throw new UniversalQueryException($"Aggregate field '{aggregate.Field}' is not available for this resource.");
        
        valueType = GetSelectorValueType(selector);
        
        return selector;
    }

    private static bool IsNumericType(Type type) => 
        Type.GetTypeCode(type) is TypeCode.Byte or TypeCode.Decimal or TypeCode.Double or TypeCode.Int16 or 
                                    TypeCode.Int32 or TypeCode.Int64 or TypeCode.SByte or TypeCode.Single or 
                                    TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64;

    private static Expression ConvertToNullableDecimal(Expression expression) => 
        expression.Type == typeof(decimal?) ? expression : Expression.Convert(expression, typeof(decimal?));

    private static Type GetSelectorValueType<TEntity>(Expression<Func<TEntity, object?>> selector) => GetSelectorValueType(selector.Body);

    private static Type GetSelectorValueType(Expression expression) => GetSelectorBody(expression).Type;

    private static Expression GetSelectorBody(Expression expression) => 
        expression.NodeType == ExpressionType.Convert && expression.Type == typeof(object) ? ((UnaryExpression)expression).Operand : expression;

    private static void ValidateRequest(UniversalQueryRequest request)
    {
        if (request.Page < 1) throw new UniversalQueryException("Page must be greater than zero.");
        if (request.PageSize is < 1 or > 250) throw new UniversalQueryException("PageSize must be between 1 and 250.");
    }

    private static IQueryable<TEntity> ApplyFilter<TEntity>(IQueryable<TEntity> query, UniversalFilterGroupRequest? group, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        if (group is null) return query;

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "entity");
        Expression? expression = BuildGroupExpression(group, parameter, fields);

        return expression is null ? query : query.Where(Expression.Lambda<Func<TEntity, bool>>(expression, parameter));
    }

    private static Expression? BuildGroupExpression<TEntity>(UniversalFilterGroupRequest group, ParameterExpression parameter, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        List<Expression> expressions = [];

        foreach (UniversalFilterConditionRequest condition in group.Conditions)
        {
            expressions.Add(BuildCondition(condition, parameter, fields));
        }

        foreach (UniversalFilterGroupRequest child in group.Groups)
        {
            Expression? childExpression = BuildGroupExpression(child, parameter, fields);

            if (childExpression is not null)
            {
                expressions.Add(childExpression);
            }
        }

        if (expressions.Count == 0) return null;

        Expression result = expressions[0];

        for (int index = 1; index < expressions.Count; index++)
        {
            result = group.Logic == UniversalFilterLogic.Or ? Expression.OrElse(result, expressions[index]) : Expression.AndAlso(result, expressions[index]);
        }
        
        return result;
    }

    private static Expression BuildCondition<TEntity>(UniversalFilterConditionRequest condition, ParameterExpression parameter, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        Expression member = GetMemberExpression(condition.Field, parameter, fields);
        Type targetType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;

        return condition.Operator switch
        {
            UniversalFilterOperator.IsNull when member.Type.IsValueType && Nullable.GetUnderlyingType(member.Type) is null => Expression.Constant(false),
            UniversalFilterOperator.IsNotNull when member.Type.IsValueType && Nullable.GetUnderlyingType(member.Type) is null => Expression.Constant(true),
            UniversalFilterOperator.IsNull => Expression.Equal(member, Expression.Constant(null, member.Type)),
            UniversalFilterOperator.IsNotNull => Expression.NotEqual(member, Expression.Constant(null, member.Type)),
            UniversalFilterOperator.In => BuildCollectionCondition(member, condition.Values, targetType, false),
            UniversalFilterOperator.NotIn => BuildCollectionCondition(member, condition.Values, targetType, true),
            UniversalFilterOperator.Contains => BuildStringCondition(member, condition.Value, nameof(string.Contains)),
            UniversalFilterOperator.StartsWith => BuildStringCondition(member, condition.Value, nameof(string.StartsWith)),
            UniversalFilterOperator.EndsWith => BuildStringCondition(member, condition.Value, nameof(string.EndsWith)),
            UniversalFilterOperator.Equal => Expression.Equal(member, ConvertValue(condition.Value, targetType, member.Type)),
            UniversalFilterOperator.NotEqual => Expression.NotEqual(member, ConvertValue(condition.Value, targetType, member.Type)),
            UniversalFilterOperator.GreaterThan => Expression.GreaterThan(member, ConvertValue(condition.Value, targetType, member.Type)),
            UniversalFilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, ConvertValue(condition.Value, targetType, member.Type)),
            UniversalFilterOperator.LessThan => Expression.LessThan(member, ConvertValue(condition.Value, targetType, member.Type)),
            UniversalFilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(member, ConvertValue(condition.Value, targetType, member.Type)),
            UniversalFilterOperator.Between => BuildBetween(member, condition, targetType),
            _ => throw new UniversalQueryException($"Unsupported filter operator '{condition.Operator}'.")
        };
    }

    private static Expression BuildStringCondition(Expression member, string? value, string methodName)
    {
        if (member.Type != typeof(string)) throw new UniversalQueryException($"Operator '{methodName}' requires a string field.");

        ConstantExpression constant = Expression.Constant(value ?? string.Empty, typeof(string));

        return Expression.Call(member, typeof(string).GetMethod(methodName, [typeof(string)])!, constant);
    }

    private static Expression BuildCollectionCondition(Expression member, IReadOnlyList<string> values, Type targetType, bool negate)
    {
        if (values.Count == 0) throw new UniversalQueryException("IN and NOT IN require at least one value.");

        Type nullableType = member.Type;
        Array array = Array.CreateInstance(nullableType, values.Count);

        for (int index = 0; index < values.Count; index++)
        {
            array.SetValue(ConvertValueObject(values[index], targetType, nullableType), index);
        }

        Expression contains = Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), [nullableType], Expression.Constant(array), member);

        return negate ? Expression.Not(contains) : contains;
    }

    private static object? ConvertValueObject(string value, Type targetType, Type memberType)
    {
        object converted = targetType.CommonConvert(value);
        
        return targetType == memberType ? converted : Activator.CreateInstance(memberType, converted);
    }

    private static Expression BuildBetween(Expression member, UniversalFilterConditionRequest condition, Type targetType)
    {
        Expression lower = ConvertValue(condition.Value, targetType, member.Type);
        Expression upper = ConvertValue(condition.ValueTo, targetType, member.Type);
        
        return Expression.AndAlso(Expression.GreaterThanOrEqual(member, lower), Expression.LessThanOrEqual(member, upper));
    }

    private static Expression GetMemberExpression<TEntity>(string field, ParameterExpression parameter, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        if (!fields.TryGetValue(field, out Expression<Func<TEntity, object?>>? selector)) throw new UniversalQueryException($"Field '{field}' is not available for this resource.");
        
        Expression body = selector.Body;
       
        if (body.NodeType == ExpressionType.Convert && body.Type == typeof(object)) body = ((UnaryExpression)body).Operand;
        
        return ReplaceParameter(body, selector.Parameters[0], parameter);
    }

    private static Expression ReplaceParameter(Expression expression, ParameterExpression source, ParameterExpression target) => new ParameterReplaceVisitor(source, target).Visit(expression)!;

    private static Expression ConvertValue(string? value, Type targetType, Type memberType)
    {
        if (value is null) return Expression.Constant(null, memberType);

        object converted = targetType.CommonConvert(value);
        Expression constant = Expression.Constant(converted, targetType);
        
        return targetType == memberType ? constant : Expression.Convert(constant, memberType);
    }

    private static object CommonConvert(this Type targetType, string value) => targetType.IsEnum
                            ? Enum.Parse(targetType, value, true)
                            : targetType == typeof(Guid)
                                ? Guid.Parse(value)
                                : targetType == typeof(DateTime)
                                    ? DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                                    : targetType == typeof(DateTimeOffset)
                                        ? DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                                        : Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture)!;

    private static IQueryable<TEntity> ApplySearch<TEntity>(IQueryable<TEntity> query, UniversalSearchRequest? search, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        if (search is null || string.IsNullOrWhiteSpace(search.Text)) return query;
        if (search.Mode == UniversalSearchMode.Levenshtein && search.LevenshteinMaxDistance is < 0 or > 20) throw new UniversalQueryException("LevenshteinMaxDistance must be between 0 and 20.");
        
        List<string> selectedFields = search.Fields.Count == 0 
                                        ? fields.Where(x => GetSelectorValueType(x.Value) == typeof(string)).Select(x => x.Key).ToList() 
                                        : search.Fields;
        List<Expression<Func<TEntity, bool>>> predicates = [];
        
        foreach (string field in selectedFields)
        {
            if (!fields.TryGetValue(field, out Expression<Func<TEntity, object?>>? selector)) throw new UniversalQueryException($"Search field '{field}' is not available for this resource.");
            
            predicates.Add(BuildSearchPredicate(selector, search));
        }
        
        return predicates.Count == 0 ? query : query.Where(CombineOr(predicates));
    }

    private static Expression<Func<TEntity, bool>> BuildSearchPredicate<TEntity>(Expression<Func<TEntity, object?>> selector, UniversalSearchRequest search)
    {
        ParameterExpression parameter = selector.Parameters[0];
        Expression member = selector.Body;

        if (member.NodeType == ExpressionType.Convert && member.Type == typeof(object)) member = ((UnaryExpression)member).Operand;
        if (member.Type != typeof(string)) throw new UniversalQueryException("The selected search field is not a string field.");

        Expression source = member;
        string comparisonText = search.Text ?? string.Empty;

        if (search.IgnoreWhitespace)
        {
            source = Expression.Call(source, nameof(string.Replace), Type.EmptyTypes, Expression.Constant(" "), Expression.Constant(string.Empty));
            comparisonText = comparisonText.Replace(" ", string.Empty, StringComparison.Ordinal);
        }

        if (search.Mode == UniversalSearchMode.Levenshtein)
        {
            if (!search.CaseSensitive)
            {
                source = Expression.Call(source, nameof(string.ToLower), Type.EmptyTypes);
                comparisonText = comparisonText.ToLowerInvariant();
            }

            Expression distance = Expression.Call(typeof(UniversalQueryDbFunctions), 
                                                    nameof(UniversalQueryDbFunctions.Levenshtein), 
                                                    Type.EmptyTypes, 
                                                    source, 
                                                    Expression.Constant(comparisonText));

            return Expression.Lambda<Func<TEntity, bool>>(Expression.LessThanOrEqual(distance, Expression.Constant(search.LevenshteinMaxDistance)), parameter);
        }

        string pattern = search.Mode switch
        {
            UniversalSearchMode.Exact => comparisonText,
            UniversalSearchMode.StartsWith => $"{comparisonText}%",
            UniversalSearchMode.EndsWith => $"%{comparisonText}",
            _ => $"%{comparisonText}%"
        };

        if (!search.CaseSensitive)
        {
            source = Expression.Call(source, nameof(string.ToLower), Type.EmptyTypes);
            pattern = pattern.ToLowerInvariant();
        }

        Expression call = Expression.Call(typeof(DbFunctionsExtensions), 
                                        nameof(DbFunctionsExtensions.Like), 
                                        Type.EmptyTypes, 
                                        Expression.Property(null, typeof(EF), nameof(EF.Functions)), source, Expression.Constant(pattern));
        
        return Expression.Lambda<Func<TEntity, bool>>(call, parameter);
    }

    private static Expression<Func<TEntity, bool>> CombineOr<TEntity>(IReadOnlyList<Expression<Func<TEntity, bool>>> predicates)
    {
        ParameterExpression parameter = predicates[0].Parameters[0];
        Expression result = predicates[0].Body;

        for (int index = 1; index < predicates.Count; index++)
        {
            result = Expression.OrElse(result, ReplaceParameter(predicates[index].Body, predicates[index].Parameters[0], parameter));
        }
        
        return Expression.Lambda<Func<TEntity, bool>>(result, parameter);
    }

    private static IQueryable<TEntity> ApplySort<TEntity>(IQueryable<TEntity> query, IReadOnlyList<UniversalSortRequest> sorts, IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> fields)
    {
        IOrderedQueryable<TEntity>? ordered = null;
        IReadOnlyList<UniversalSortRequest> effectiveSorts = sorts.Count == 0 && fields.ContainsKey("id") 
                                                                ? [new UniversalSortRequest { Field = "id", Descending = false }] 
                                                                : sorts;
        
        foreach (UniversalSortRequest sort in effectiveSorts)
        {
            if (!fields.TryGetValue(sort.Field, out Expression<Func<TEntity, object?>>? selector)) throw new UniversalQueryException($"Sort field '{sort.Field}' is not available for this resource.");
            
            ordered = ordered is null 
                        ? (sort.Descending ? query.OrderByDescending(selector) : query.OrderBy(selector)) 
                        : (sort.Descending ? ordered.ThenByDescending(selector) : ordered.ThenBy(selector));
        }

        return ordered ?? query;
    }

    private sealed class ParameterReplaceVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression source;
        private readonly ParameterExpression target;

        public ParameterReplaceVisitor(ParameterExpression source, ParameterExpression target) 
        { 
            this.source = source; 
            this.target = target; 
        }

        protected override Expression VisitParameter(ParameterExpression node) => node == source ? target : base.VisitParameter(node);
    }
}

/// <summary>
/// Internal projection used by the universal grouped aggregate query.
/// </summary>
internal sealed class UniversalAggregateProjection<TKey>
{
    /// <summary>
    /// Gets or sets the grouping value.
    /// </summary>
    public TKey GroupValue { get; set; } = default!;
    /// <summary>
    /// Gets or sets the first aggregate.
    /// </summary>
    public decimal? Value1 { get; set; }
    /// <summary>
    /// Gets or sets the second aggregate.
    /// </summary>
    public decimal? Value2 { get; set; }
    /// <summary>
    /// Gets or sets the third aggregate.
    /// </summary>
    public decimal? Value3 { get; set; }
    /// <summary>
    /// Gets or sets the fourth aggregate.
    /// </summary>
    public decimal? Value4 { get; set; }
    /// <summary>
    /// Gets or sets the fifth aggregate.
    /// </summary>
    public decimal? Value5 { get; set; }
    /// <summary>
    /// Gets or sets the sixth aggregate.
    /// </summary>
    public decimal? Value6 { get; set; }
    /// <summary>
    /// Gets or sets the seventh aggregate.
    /// </summary>
    public decimal? Value7 { get; set; }
    /// <summary>
    /// Gets or sets the eighth aggregate.
    /// </summary>
    public decimal? Value8 { get; set; }
}
