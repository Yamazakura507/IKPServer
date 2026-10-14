using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Domain.Entities.Employees;
using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region DbSets
        #region App
        public DbSet<Employee> Employees => Set<Employee>();
        #endregion
        #region Logs
        #region Lookup
        public DbSet<AuditActionDefinition> AuditActionDefinitions => Set<AuditActionDefinition>();
        #endregion

        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<AuditLogChange> AuditLogChanges => Set<AuditLogChange>();
        #endregion
        #region Diagnostics
        #region Core
        public DbSet<ErrorGroup> ErrorGroups => Set<ErrorGroup>();
        public DbSet<ErrorOccurrence> ErrorOccurrences => Set<ErrorOccurrence>();
        #region Criticality
        public DbSet<ErrorCriticality> ErrorCriticalities => Set<ErrorCriticality>();
        public DbSet<ErrorCriticalityFactor> ErrorCriticalityFactors => Set<ErrorCriticalityFactor>();
        #endregion
        #region Area
        public DbSet<DiagnosticAreaResolution> DiagnosticAreaResolutions => Set<DiagnosticAreaResolution>();
        public DbSet<DiagnosticAreaResolutionEvidence> DiagnosticAreaResolutionEvidences => Set<DiagnosticAreaResolutionEvidence>();
        #endregion
        #endregion
        #region Statistics
        public DbSet<ErrorStatistics> ErrorStatistics => Set<ErrorStatistics>();
        public DbSet<ErrorMetricBucket> ErrorMetricBuckets => Set<ErrorMetricBucket>();
        public DbSet<DiagnosticStatisticsWorkItem> DiagnosticStatisticsWorkItems => Set<DiagnosticStatisticsWorkItem>();
        #endregion
        #region Lookup
        #region Area
        public DbSet<DiagnosticArea> DiagnosticAreas => Set<DiagnosticArea>();
        public DbSet<DiagnosticAreaResolutionSource> DiagnosticAreaResolutionSources => Set<DiagnosticAreaResolutionSource>();
        public DbSet<DiagnosticAreaResolutionRule> DiagnosticAreaResolutionRules => Set<DiagnosticAreaResolutionRule>();
        public DbSet<DiagnosticAreaResolutionMapping> DiagnosticAreaResolutionMappings => Set<DiagnosticAreaResolutionMapping>();
        public DbSet<DiagnosticStatisticsWorkItemStatus> DiagnosticStatisticsWorkItemStatuses => Set<DiagnosticStatisticsWorkItemStatus>();
        #endregion
        public DbSet<DiagnosticDefinitionGroup> DiagnosticDefinitionGroups => Set<DiagnosticDefinitionGroup>();
        public DbSet<DiagnosticDefinition> DiagnosticDefinitions => Set<DiagnosticDefinition>();
        #endregion
        #endregion
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(DatabaseSchemas.App);
            modelBuilder.HasDbFunction(() => UniversalQueryDbFunctions.Levenshtein("", "")).HasName("levenshtein").IsBuiltIn();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.OnSeeds();
        }
    }
}
