using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Domain.Entities.Diagnostics.Statistics
{
    public class ErrorMetricBucket : Entity
    {
        public Guid ErrorGroupId { get; set; }

        public DateTime BucketStart { get; set; }

        public int BucketDurationMinutes { get; set; }

        public Guid BucketTypeId { get; set; }

        public long Occurrences { get; set; }

        public long UniqueUsers { get; set; }

        public DateTime? FirstOccurrenceAt { get; set; }

        public DateTime? LastOccurrenceAt { get; set; }

        public DateTime CalculatedAt { get; set; }

        public ErrorGroup ErrorGroup { get; set; } = null!;

        public DiagnosticDefinition BucketTypeDefinition { get; set; } = null!;
    }
}
