using IKP.Domain.Common.Base;
using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Domain.Entities.Diagnostics.Criticality;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Domain.Entities.Diagnostics.Statistics;

namespace IKP.Domain.Entities.Diagnostics.Errors
{
    public class ErrorGroup : Entity
    {
        public string Fingerprint { get; set; } = null!;

        public int FingerprintVersion { get; set; }

        public string ApplicationId { get; set; } = null!;

        public Guid? DiagnosticAreaId { get; set; }

        public DiagnosticArea? DiagnosticArea { get; set; }

        public string ExceptionType { get; set; } = null!;

        public string? ErrorCode { get; set; }

        public string Title { get; set; } = null!;

        public string? Summary { get; set; }

        public Guid SeverityId { get; set; }

        public Guid StatusId { get; set; }

        public DateTime FirstOccurredAt { get; set; }

        public DateTime LastOccurredAt { get; set; }

        public string? FirstSeenVersion { get; set; }

        public string? LastSeenVersion { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ErrorCriticality? Criticality { get; set; }

        public ErrorStatistics? Statistics { get; set; }

        public DiagnosticDefinition StatusDefinition { get; set; } = null!;

        public DiagnosticDefinition SeverityDefinition { get; set; } = null!;

        public ICollection<ErrorOccurrence> Occurrences { get; set; } = new List<ErrorOccurrence>();
    }
}
