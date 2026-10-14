using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Criticality.Models;
using IKP.Application.Diagnostics.Resolvers.Models;
using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IKP.Infrastructure.Services.Diagnostics
{
    /// <summary>
    /// Provides Entity Framework persistence for diagnostics.
    /// </summary>
    public class DiagnosticRepository : IDiagnosticRepository
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private AppDbContext? dbContext;
        private IDbContextTransaction? dbTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticRepository"/> class.
        /// </summary>
        /// <param name="dbContextFactory">Application database context factory.</param>
        public DiagnosticRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc />
        public async ValueTask<ErrorGroup?> FindErrorGroupAsync(string fingerprint, int fingerprintVersion, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);

            dbTransaction ??= await context.Database.BeginTransactionAsync(cancellationToken);

            string lockKey = $"{fingerprint}:{fingerprintVersion}";
            await context.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended(CAST({lockKey} AS text), 0));", cancellationToken);

            return await context.ErrorGroups
                            .Include(x => x.DiagnosticAreaResolution)
                            .FirstOrDefaultAsync(x => x.Fingerprint == fingerprint && x.FingerprintVersion == fingerprintVersion, cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask<Guid> GetDefinitionIdAsync(string groupCode, string definitionCode, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);

            Guid? definitionId = await context.DiagnosticDefinitions
                .Where(x => x.Code == definitionCode && x.IsActive && x.Group.Code == groupCode && x.Group.IsActive)
                .Select(x => (Guid?)x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return definitionId ?? throw new InvalidOperationException($"Diagnostic definition '{definitionCode}' was not found in group '{groupCode}'.");
        }


        /// <inheritdoc />
        public async ValueTask<Guid> GetDiagnosticAreaIdAsync(string code, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);
            Guid areaId = await context.DiagnosticAreas.Where(x => x.Code == code && x.IsActive).Select(x => x.Id).SingleOrDefaultAsync(cancellationToken);

            if (areaId == Guid.Empty) throw new InvalidOperationException($"Active diagnostic area '{code}' was not found.");

            return areaId;
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<DiagnosticAreaResolutionMappingModel>> GetActiveDiagnosticAreaResolutionMappingsAsync(CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);

            return await context.DiagnosticAreaResolutionMappings
                .AsNoTracking()
                .Where(x => x.IsActive && x.DiagnosticArea.IsActive && x.Rule.IsActive)
                .Select(x => new DiagnosticAreaResolutionMappingModel
                {
                    Id = x.Id,
                    DiagnosticAreaId = x.DiagnosticAreaId,
                    RuleId = x.RuleId,
                    RuleCode = x.Rule.Code,
                    Pattern = x.Pattern,
                    Priority = x.Priority,
                    Confidence = x.Confidence
                })
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask<DiagnosticAreaResolution> CreateAutomaticDiagnosticAreaResolutionAsync(
            Guid diagnosticAreaId, Guid primaryRuleId, decimal confidence, int version, IReadOnlyList<DiagnosticAreaResolutionEvidenceModel> evidences, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);
            Guid sourceId = await context.DiagnosticAreaResolutionSources
                .Where(x => x.Code == DiagnosticAreaResolutionSourceCodes.Automatic && x.IsActive)
                .Select(x => (Guid?)x.Id)
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new InvalidOperationException($"Active diagnostic area resolution source '{DiagnosticAreaResolutionSourceCodes.Automatic}' was not found.");

            DiagnosticAreaResolution resolution = new()
            {
                DiagnosticAreaId = diagnosticAreaId,
                SourceId = sourceId,
                RuleId = primaryRuleId,
                Confidence = confidence,
                Version = version
            };

            context.DiagnosticAreaResolutions.Add(resolution);

            foreach (DiagnosticAreaResolutionEvidenceModel evidenceModel in evidences)
            {
                DiagnosticAreaResolutionEvidence evidence = new()
                {
                    DiagnosticAreaResolution = resolution,
                    MappingId = evidenceModel.MappingId,
                    RuleId = evidenceModel.RuleId,
                    Pattern = evidenceModel.Pattern,
                    MatchedValue = evidenceModel.MatchedValue,
                    Priority = evidenceModel.Priority,
                    Confidence = evidenceModel.Confidence,
                    Contribution = evidenceModel.Contribution
                };

                context.DiagnosticAreaResolutionEvidences.Add(evidence);
            }

            return resolution;
        }

        /// <inheritdoc />
        public async ValueTask<DiagnosticAreaResolution?> CreateDiagnosticAreaResolutionAsync(Guid? diagnosticAreaId, string sourceCode, string ruleCode, decimal confidence, int version, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);
            Guid? sourceId = await context.DiagnosticAreaResolutionSources.Where(x => x.Code == sourceCode && x.IsActive).Select(x => (Guid?)x.Id).SingleOrDefaultAsync(cancellationToken);

            if (!sourceId.HasValue) throw new InvalidOperationException($"Active diagnostic area resolution source '{sourceCode}' was not found.");

            Guid? ruleId = await context.DiagnosticAreaResolutionRules.Where(x => x.Code == ruleCode && x.IsActive).Select(x => (Guid?)x.Id).SingleOrDefaultAsync(cancellationToken);

            if (!ruleId.HasValue) throw new InvalidOperationException($"Active diagnostic area resolution rule '{ruleCode}' was not found.");

            DiagnosticAreaResolution resolution = new()
            {
                DiagnosticAreaId = diagnosticAreaId,
                SourceId = sourceId,
                RuleId = ruleId,
                Confidence = confidence,
                Version = version
            };

            context.DiagnosticAreaResolutions.Add(resolution);

            return resolution;
        }


        /// <inheritdoc />
        public async ValueTask ScheduleStatisticsWorkItemAsync(Guid errorGroupId, DateTime requestedAt, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);

            dbTransaction ??= await context.Database.BeginTransactionAsync(cancellationToken);

            IDbContextTransaction transaction = dbTransaction;

            try
            {
                await context.SaveChangesAsync(cancellationToken);

                await context.Database.ExecuteSqlInterpolatedAsync(
                    $"""
            INSERT INTO "diagnostics_core"."DiagnosticStatisticsWorkItems"
            (
                "Id",
                "ErrorGroupId",
                "RequestedAt",
                "AvailableAt",
                "StartedAt",
                "CompletedAt",
                "AttemptCount",
                "StatusId",
                "LastError"
            )
            VALUES
            (
                gen_random_uuid(),
                {errorGroupId},
                {requestedAt},
                {requestedAt},
                NULL,
                NULL,
                0,
                (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Pending} AND "IsActive" LIMIT 1),
                NULL
            )
            ON CONFLICT ("ErrorGroupId")
            DO UPDATE SET
                "RequestedAt" = EXCLUDED."RequestedAt",
                "CompletedAt" = NULL,
                "LastError" = NULL,
                "StatusId" = CASE
                    WHEN "DiagnosticStatisticsWorkItems"."StatusId" = (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Processing} AND "IsActive" LIMIT 1)
                    THEN "DiagnosticStatisticsWorkItems"."StatusId"
                    ELSE (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Pending} AND "IsActive" LIMIT 1)
                END,
                "AvailableAt" = CASE
                    WHEN "DiagnosticStatisticsWorkItems"."StatusId" = (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Processing} AND "IsActive" LIMIT 1)
                    THEN "DiagnosticStatisticsWorkItems"."AvailableAt"
                    ELSE EXCLUDED."AvailableAt"
                END,
                "StartedAt" = CASE
                    WHEN "DiagnosticStatisticsWorkItems"."StatusId" = (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Processing} AND "IsActive" LIMIT 1)
                    THEN "DiagnosticStatisticsWorkItems"."StartedAt"
                    ELSE NULL
                END,
                "AttemptCount" = CASE
                    WHEN "DiagnosticStatisticsWorkItems"."StatusId" = (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Processing} AND "IsActive" LIMIT 1)
                    THEN "DiagnosticStatisticsWorkItems"."AttemptCount"
                    ELSE 0
                END;
            """,
                    cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                await transaction.DisposeAsync();
                dbTransaction = null;
                throw;
            }
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<DiagnosticStatisticsWorkItem>> ClaimStatisticsWorkItemsAsync(int batchSize, DateTime now, TimeSpan staleAfter, CancellationToken cancellationToken = default)
        {
            if (batchSize <= 0) throw new ArgumentOutOfRangeException(nameof(batchSize));

            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            DateTime staleBefore = now.Subtract(staleAfter);
            Dictionary<string, Guid> statusIds = await context.DiagnosticStatisticsWorkItemStatuses
                .AsNoTracking()
                .Where(x => x.IsActive && (x.Code == DiagnosticStatisticsWorkItemStatusCodes.Pending || 
                                            x.Code == DiagnosticStatisticsWorkItemStatusCodes.Processing || 
                                            x.Code == DiagnosticStatisticsWorkItemStatusCodes.Completed || 
                                            x.Code == DiagnosticStatisticsWorkItemStatusCodes.Failed))
                .ToDictionaryAsync(x => x.Code, x => x.Id, cancellationToken);

            Guid pendingStatusId = GetRequiredStatusId(statusIds, DiagnosticStatisticsWorkItemStatusCodes.Pending);
            Guid processingStatusId = GetRequiredStatusId(statusIds, DiagnosticStatisticsWorkItemStatusCodes.Processing);
            Guid failedStatusId = GetRequiredStatusId(statusIds, DiagnosticStatisticsWorkItemStatusCodes.Failed);

            await context.DiagnosticStatisticsWorkItems
                .Where(x => x.StatusId == processingStatusId && x.StartedAt.HasValue && x.StartedAt.Value < staleBefore)
                .ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(x => x.StatusId, pendingStatusId)
                        .SetProperty(x => x.AvailableAt, now)
                        .SetProperty(x => x.StartedAt, (DateTime?)null),
                    cancellationToken);

            List<DiagnosticStatisticsWorkItem> workItems = await context.DiagnosticStatisticsWorkItems
                .FromSqlInterpolated($"""
                    SELECT *
                    FROM "diagnostics_core"."DiagnosticStatisticsWorkItems"
                    WHERE "StatusId" IN ({pendingStatusId}, {failedStatusId})
                      AND "AvailableAt" <= {now}
                    ORDER BY "AvailableAt", "RequestedAt"
                    FOR UPDATE SKIP LOCKED
                    LIMIT {batchSize}
                    """)
                .ToListAsync(cancellationToken);

            foreach (DiagnosticStatisticsWorkItem workItem in workItems)
            {
                workItem.StatusId = processingStatusId;
                workItem.StartedAt = now;
                workItem.AttemptCount++;
                workItem.LastError = null;
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return workItems;
        }

        private static Guid GetRequiredStatusId(IReadOnlyDictionary<string, Guid> statusIds, string code)
        {
            if (statusIds.TryGetValue(code, out Guid statusId)) return statusId;

            throw new InvalidOperationException($"Active diagnostic statistics work item status '{code}' was not found.");
        }

        /// <inheritdoc />
        public async ValueTask CompleteStatisticsWorkItemAsync(Guid workItemId, DateTime claimedRequestedAt, DateTime completedAt, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            await context.Database.ExecuteSqlInterpolatedAsync(
                $"""
                UPDATE "diagnostics_core"."DiagnosticStatisticsWorkItems"
                SET
                    "StatusId" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Pending} AND "IsActive" LIMIT 1)
                        ELSE (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Completed} AND "IsActive" LIMIT 1)
                    END,
                    "AvailableAt" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN {completedAt}
                        ELSE "AvailableAt"
                    END,
                    "StartedAt" = NULL,
                    "CompletedAt" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN NULL
                        ELSE {completedAt}
                    END,
                    "AttemptCount" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN 0
                        ELSE "AttemptCount"
                    END
                WHERE "Id" = {workItemId};
                """,
                cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask FailStatisticsWorkItemAsync(Guid workItemId, DateTime claimedRequestedAt, string error, DateTime availableAt, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            string failure = error.Length <= 4000 ? error : error[..4000];

            await context.Database.ExecuteSqlInterpolatedAsync(
                $"""
                UPDATE "diagnostics_core"."DiagnosticStatisticsWorkItems"
                SET
                    "StatusId" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Pending} AND "IsActive" LIMIT 1)
                        ELSE (SELECT "Id" FROM "diagnostics_lookup"."DiagnosticStatisticsWorkItemStatuses" WHERE "Code" = {DiagnosticStatisticsWorkItemStatusCodes.Failed} AND "IsActive" LIMIT 1)
                    END,
                    "AvailableAt" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN {DateTime.UtcNow}
                        ELSE {availableAt}
                    END,
                    "StartedAt" = NULL,
                    "LastError" = {failure},
                    "AttemptCount" = CASE
                        WHEN "RequestedAt" > {claimedRequestedAt}
                        THEN 0
                        ELSE "AttemptCount"
                    END
                WHERE "Id" = {workItemId};
                """,
                cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask RecalculateStatisticsAsync(Guid errorGroupId, DateTime calculatedAt, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            await context.Database.ExecuteSqlInterpolatedAsync($"SELECT diagnostics_statistics.\"RecalculateErrorStatistics\"({errorGroupId}, {calculatedAt});", cancellationToken);
        }


        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<Guid>> GetCriticalityRefreshCandidatesAsync(int batchSize, CancellationToken cancellationToken = default)
        {
            if (batchSize <= 0) throw new ArgumentOutOfRangeException(nameof(batchSize));

            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            return await context.ErrorGroups
                .AsNoTracking()
                .Where(x => x.Statistics != null && (x.Criticality == null || x.Criticality.CalculatedAt < x.Statistics.CalculatedAt))
                .OrderBy(x => x.Criticality == null ? DateTime.MinValue : x.Criticality.CalculatedAt)
                .ThenBy(x => x.Statistics!.CalculatedAt)
                .Select(x => x.Id)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask<DiagnosticCriticalityCalculationInput> GetCriticalityCalculationInputAsync(Guid errorGroupId, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            var data = await context.ErrorGroups
                .AsNoTracking()
                .Where(x => x.Id == errorGroupId)
                .Select(x => new
                {
                    Statistics = x.Statistics,
                    FeatureImportance = x.DiagnosticAreaResolution != null && x.DiagnosticAreaResolution.DiagnosticArea != null
                        ? (decimal?)x.DiagnosticAreaResolution.DiagnosticArea.ImportanceScore : null,
                    ManualScore = x.Criticality != null ? x.Criticality.ManualScore : null
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (data is null || data.Statistics is null) throw new InvalidOperationException($"Statistics for error group '{errorGroupId}' were not found.");

            return new DiagnosticCriticalityCalculationInput
            {
                OccurrencesLastHour = data.Statistics.OccurrencesLastHour,
                UsersLastHour = data.Statistics.UsersLastHour,
                AffectedUserRatio = data.Statistics.AffectedUserRatio,
                GrowthRate = data.Statistics.GrowthRate,
                StatisticsCalculatedAt = data.Statistics.CalculatedAt,
                FeatureImportance = data.FeatureImportance,
                ManualScore = data.ManualScore
            };
        }

        /// <inheritdoc />
        public async ValueTask SaveCriticalityAsync(Guid errorGroupId, DateTime calculatedAt, DiagnosticCriticalityCalculationResult result, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            await context.Database.ExecuteSqlInterpolatedAsync($"SELECT pg_advisory_xact_lock(hashtextextended(CAST({errorGroupId.ToString()} AS text), 1));", cancellationToken);

            Dictionary<string, Guid> definitionIds = await context.DiagnosticDefinitions
                .AsNoTracking()
                .Where(x => x.IsActive && x.Group.Code == DiagnosticDefinitionGroupCodes.CriticalityFactor)
                .ToDictionaryAsync(x => x.Code, x => x.Id, cancellationToken);

            ErrorCriticality? criticality = await context.ErrorCriticalities
                .Include(x => x.Factors)
                .SingleOrDefaultAsync(x => x.ErrorGroupId == errorGroupId, cancellationToken);

            if (criticality is null)
            {
                criticality = new ErrorCriticality
                {
                    ErrorGroupId = errorGroupId,
                    AutomaticScore = result.AutomaticScore,
                    EffectiveScore = result.AutomaticScore,
                    CalculationVersion = 1,
                    CalculatedAt = calculatedAt
                };

                context.ErrorCriticalities.Add(criticality);
            }
            else
            {
                criticality.AutomaticScore = result.AutomaticScore;
                criticality.EffectiveScore = criticality.ManualScore ?? result.AutomaticScore;
                criticality.CalculationVersion = 1;
                criticality.CalculatedAt = calculatedAt;
            }

            decimal totalAvailableWeight = result.Factors.Where(x => x.IsAvailable).Sum(x => x.Weight);

            foreach (DiagnosticCriticalityFactorResult factorResult in result.Factors)
            {
                if (!definitionIds.TryGetValue(factorResult.Code, out Guid definitionId)) throw new InvalidOperationException($"Active criticality factor definition '{factorResult.Code}' was not found.");

                ErrorCriticalityFactor? factor = criticality.Factors.FirstOrDefault(x => x.DefinitionId == definitionId);

                if (factor is null)
                {
                    factor = new ErrorCriticalityFactor
                    {
                        ErrorCriticalityId = criticality.Id,
                        DefinitionId = definitionId
                    };

                    criticality.Factors.Add(factor);
                }

                factor.Score = factorResult.Score;
                factor.Weight = factorResult.Weight;
                factor.Value = factorResult.Value;
                factor.Contribution = factorResult.IsAvailable && totalAvailableWeight > 0 ? Math.Round(factorResult.Score * factorResult.Weight / totalAvailableWeight, 4) : 0;
                factor.CalculatedAt = calculatedAt;
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }


        /// <inheritdoc />
        public void AddErrorGroup(ErrorGroup errorGroup)
        {
            AppDbContext context = GetRequiredDbContext();

            context.ErrorGroups.Add(errorGroup);
        }

        /// <inheritdoc />
        public void AddErrorOccurrence(ErrorOccurrence errorOccurrence)
        {
            AppDbContext context = GetRequiredDbContext();

            context.ErrorOccurrences.Add(errorOccurrence);
        }

        /// <inheritdoc />
        public async ValueTask SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AppDbContext context = GetRequiredDbContext();

            if (dbTransaction is null)
            {
                await context.SaveChangesAsync(cancellationToken);
                return;
            }

            IDbContextTransaction transaction = dbTransaction;

            try
            {
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                await transaction.DisposeAsync();

                dbTransaction = null;
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                await transaction.DisposeAsync();

                dbTransaction = null;
                throw;
            }
        }

        /// <summary>
        /// Gets or creates the database context used by this repository.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The database context.</returns>
        private async ValueTask<AppDbContext> GetDbContextAsync(CancellationToken cancellationToken)
        {
            if (dbContext is not null) return dbContext;

            dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            return dbContext;
        }

        /// <summary>
        /// Gets the initialized database context.
        /// </summary>
        /// <returns>The database context.</returns>
        private AppDbContext GetRequiredDbContext() => dbContext ?? throw new InvalidOperationException("The diagnostic repository has not initialized its database context.");
    }
}
