namespace IKP.Infrastructure.Services.Extensions
{
    internal static class TypeExtensions
    {
        private static IReadOnlyDictionary<Type, string> typesInStrings = new Dictionary<Type, string>
        {
            [typeof(string)] = "String",
            [typeof(int)] = "Int32",
            [typeof(long)] = "Int64",
            [typeof(decimal)] = "Decimal",
            [typeof(double)] = "Double",
            [typeof(float)] = "Single",
            [typeof(bool)] = "Boolean",
            [typeof(Guid)] = "Guid",
            [typeof(DateTime)] = "DateTime",
            [typeof(DateOnly)] = "DateOnly",
            [typeof(TimeOnly)] = "TimeOnly"
        };


        public static string GetPropertyType(this Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return typesInStrings.TryGetValue(type, out string? typeString) 
                ? typeString 
                : type.IsEnum 
                    ? "Enum" 
                    : "Json";
        }
    }
}
