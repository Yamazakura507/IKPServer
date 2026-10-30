using IKP.Domain.Entities.App.HR;
using IKP.Domain.Entities.App.HR.CompetencyMatrix;
using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Attestation;
using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development;
using IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Probation;
using IKP.Domain.Entities.App.HR.Lookup;
using IKP.Domain.Entities.Auth.Credentials;
using IKP.Domain.Entities.Auth.Lookup;
using IKP.Domain.Entities.Auth.Permissions;
using IKP.Domain.Entities.Auth.Roles;
using IKP.Domain.Entities.Auth.Sessions;
using IKP.Domain.Entities.Auth.Users;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Domain.Entities.Diagnostics.Statistics;
using IKP.Domain.Entities.Files;
using IKP.Domain.Entities.Localization;
using IKP.Domain.Entities.Logs;
using IKP.Domain.Entities.Monitoring;
using IKP.Domain.Entities.Reports;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Domain.Entities.Support;
using IKP.Domain.Entities.Support.Lookups;
using IKP.Domain.Entities.System;
using IKP.Domain.Entities.Updates;
using IKP.Domain.Entities.Updates.Lookups;
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
        #region Authentication
        #region Lookup
        public DbSet<AuthRole> AuthRoles => Set<AuthRole>();
        public DbSet<AuthPermission> AuthPermissions => Set<AuthPermission>();
        public DbSet<AuthenticationMethodDefinition> AuthenticationMethods => Set<AuthenticationMethodDefinition>();
        #endregion
        public DbSet<AuthUser> AuthUsers => Set<AuthUser>();
        public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();
        public DbSet<AuthUserRole> AuthUserRoles => Set<AuthUserRole>();
        public DbSet<AuthRolePermission> AuthRolePermissions => Set<AuthRolePermission>();
        #endregion
        #region App
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<EmploymentHistory> EmployeeEmploymentHistory => Set<EmploymentHistory>();
        public DbSet<EmployeeContact> EmployeeContacts => Set<EmployeeContact>();
        public DbSet<EmployeeEducation> EmployeeEducation => Set<EmployeeEducation>();
        public DbSet<EmployeeQualification> EmployeeQualifications => Set<EmployeeQualification>();
        #region CompetencyMatrix
        public DbSet<CompetencyMatrix> CompetencyMatrices => Set<CompetencyMatrix>();
        public DbSet<CompetencyMatrixVersion> CompetencyMatrixVersions => Set<CompetencyMatrixVersion>();
        public DbSet<CompetencyMatrixSection> CompetencyMatrixSections => Set<CompetencyMatrixSection>();
        public DbSet<CompetencyMatrixItem> CompetencyMatrixItems => Set<CompetencyMatrixItem>();
        public DbSet<CompetencyMatrixTarget> CompetencyMatrixTargets => Set<CompetencyMatrixTarget>();
        public DbSet<CompetencyAssessment> CompetencyAssessments => Set<CompetencyAssessment>();
        public DbSet<CompetencyAssessmentItem> CompetencyAssessmentItems => Set<CompetencyAssessmentItem>();
        public DbSet<CompetencyAssessmentItemValue> CompetencyAssessmentItemValues => Set<CompetencyAssessmentItemValue>();
        #endregion
        #region DevelopmentAndAttestation
        #region Probation
        public DbSet<ProbationPlan> ProbationPlans => Set<ProbationPlan>();
        public DbSet<ProbationTask> ProbationTasks => Set<ProbationTask>();
        #endregion
        #region Development
        public DbSet<DevelopmentPlan> DevelopmentPlans => Set<DevelopmentPlan>();
        public DbSet<DevelopmentPlanItem> DevelopmentPlanItems => Set<DevelopmentPlanItem>();
        public DbSet<ResponsibleEmployeesOfPlanItem> ResponsibleEmployeesOfPlanItem => Set<ResponsibleEmployeesOfPlanItem>();
        #endregion
        #region Attestation
        public DbSet<Attestation> Attestations => Set<Attestation>();
        public DbSet<AttestationSection> AttestationSections => Set<AttestationSection>();
        public DbSet<AttestationQuestion> AttestationQuestions => Set<AttestationQuestion>();
        public DbSet<AttestationAnswerOption> AttestationAnswerOptions => Set<AttestationAnswerOption>();
        #endregion
        #endregion
        #region Lookup
        public DbSet<AttestationQuestionType> AttestationQuestionType => Set<AttestationQuestionType>();
        public DbSet<AttestationSectionType> AttestationSectionType => Set<AttestationSectionType>();
        public DbSet<CompetencyAssessmentType> CompetencyAssessmentType => Set<CompetencyAssessmentType>();
        public DbSet<CompetencySectionType> CompetencySectionType => Set<CompetencySectionType>();
        public DbSet<DevelopmentControlMethods> DevelopmentControlMethods => Set<DevelopmentControlMethods>();
        public DbSet<DevelopmentItemType> DevelopmentItemType => Set<DevelopmentItemType>();
        public DbSet<DevelopmentMethods> DevelopmentMethods => Set<DevelopmentMethods>();
        public DbSet<DevelopmentNeeds> DevelopmentNeeds => Set<DevelopmentNeeds>();
        public DbSet<DevelopmentPlanType> DevelopmentPlanType => Set<DevelopmentPlanType>();
        public DbSet<EducationType> EducationType => Set<EducationType>();
        public DbSet<EmployeeContactType> EmployeeContactType => Set<EmployeeContactType>();
        public DbSet<EmploymentRecordType> EmploymentRecordType => Set<EmploymentRecordType>();
        public DbSet<Position> Positions => Set<Position>();
        public DbSet<Grade> Grades => Set<Grade>();
        #endregion
        #endregion
        #region Localization
        public DbSet<Language> Languages => Set<Language>();
        #region Lookup
        public DbSet<LanguagePackage> LanguagePackages => Set<LanguagePackage>();
        #endregion
        #endregion
        #region Updates
        #region Lookup
        public DbSet<UpdateCategory> UpdateCategories => Set<UpdateCategory>();
        public DbSet<UpdatePlatform> UpdatePlatforms => Set<UpdatePlatform>();
        public DbSet<UpdateProduct> UpdateProducts => Set<UpdateProduct>();
        #endregion

        public DbSet<UpdateVersion> UpdateVersions => Set<UpdateVersion>();
        public DbSet<UpdateReleaseNote> UpdateReleaseNotes => Set<UpdateReleaseNote>();
        public DbSet<UpdateReleaseNoteAttachment> UpdateReleaseNoteAttachments => Set<UpdateReleaseNoteAttachment>();
        public DbSet<UpdateDownloadLink> UpdateDownloadLinks => Set<UpdateDownloadLink>();
        public DbSet<UpdateProductRelation> UpdateProductRelations => Set<UpdateProductRelation>();
        public DbSet<UpdateProductPermission> UpdateProductPermissions => Set<UpdateProductPermission>();
        #endregion
        #region Reports
        public DbSet<ReportTemplateVersion> ReportTemplateVersions => Set<ReportTemplateVersion>();
        public DbSet<ReportExecution> ReportExecutions => Set<ReportExecution>();
        public DbSet<ReportDefinition> ReportDefinitions => Set<ReportDefinition>();
        public DbSet<ReportParameter> ReportParameters => Set<ReportParameter>();
        #region Lookup
        public DbSet<ReportCategory> ReportCategories => Set<ReportCategory>();
        public DbSet<ReportDataType> ReportDataTypies => Set<ReportDataType>();
        public DbSet<ReportExecutionStatus> ReportExecutionStatuses => Set<ReportExecutionStatus>();
        #endregion
        #endregion
        #region System
        public DbSet<BackgroundJob> BackgroundJobs => Set<BackgroundJob>();
        public DbSet<HealthSnapshot> HealthSnapshots => Set<HealthSnapshot>();
        public DbSet<RetentionPolicy> RetentionPolicies => Set<RetentionPolicy>();
        public DbSet<BackupRecord> BackupRecords => Set<BackupRecord>();
        public DbSet<AlertRule> AlertRules => Set<AlertRule>();
        public DbSet<Alert> Alerts => Set<Alert>();
        #endregion
        #region Files
        public DbSet<FileBlob> FileBlobs => Set<FileBlob>();
        public DbSet<StoredFile> Files => Set<StoredFile>();
        public DbSet<FileUploadSession> FileUploadSessions => Set<FileUploadSession>();
        #endregion
        #region Support
        public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
        public DbSet<SupportTicketMessage> SupportTicketMessages => Set<SupportTicketMessage>();
        public DbSet<SupportTicketMessageAttachment> SupportTicketMessageAttachments => Set<SupportTicketMessageAttachment>();
        public DbSet<SupportTicketAssignment> SupportTicketAssignments => Set<SupportTicketAssignment>();
        #region Lookup
        public DbSet<SupportTicketStatus> SupportTicketStatuses => Set<SupportTicketStatus>();
        public DbSet<SupportTicketPriority> SupportTicketPriorities => Set<SupportTicketPriority>();
        public DbSet<SupportTicketCategory> SupportTicketCategories => Set<SupportTicketCategory>();
        #endregion
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
