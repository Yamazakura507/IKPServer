using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Domain.Entities.Employees;
using IKP.Domain.Entities.Logs;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using IKP.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<AuditActionDefinition> AuditActionDefinitions => Set<AuditActionDefinition>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<AuditLogChange> AuditLogChanges => Set<AuditLogChange>();

        public DbSet<ErrorGroup> ErrorGroups => Set<ErrorGroup>();
        public DbSet<ErrorOccurrence> ErrorOccurrences => Set<ErrorOccurrence>();
        public DbSet<ErrorCriticality> ErrorCriticalities => Set<ErrorCriticality>();
        public DbSet<ErrorCriticalityFactor> ErrorCriticalityFactors => Set<ErrorCriticalityFactor>();
        public DbSet<ErrorStatistics> ErrorStatistics => Set<ErrorStatistics>();
        public DbSet<ErrorMetricBucket> ErrorMetricBuckets => Set<ErrorMetricBucket>();
        public DbSet<DiagnosticArea> DiagnosticAreas => Set<DiagnosticArea>();
        public DbSet<DiagnosticDefinitionGroup> DiagnosticDefinitionGroups => Set<DiagnosticDefinitionGroup>();
        public DbSet<DiagnosticDefinition> DiagnosticDefinitions => Set<DiagnosticDefinition>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(DatabaseSchemas.App);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            AuditActionSeed.Seed(modelBuilder);
            DiagnosticDefinitionSeed.Seed(modelBuilder);
        }
    }
}
