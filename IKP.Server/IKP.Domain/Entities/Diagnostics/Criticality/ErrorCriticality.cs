using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Domain.Entities.Diagnostics.Criticality
{
    public class ErrorCriticality : Entity
    {
        public Guid ErrorGroupId { get; set; }

        public ErrorGroup ErrorGroup { get; set; } = null!;

        public decimal AutomaticScore { get; set; }

        public decimal? ManualScore { get; set; }

        public decimal EffectiveScore { get; set; }

        public int CalculationVersion { get; set; }

        public DateTime CalculatedAt { get; set; }

        public Guid? OverrideUserId { get; set; }

        public DateTime? OverrideAt { get; set; }

        public string? OverrideReason { get; set; }

        public ICollection<ErrorCriticalityFactor> Factors { get; set; } = new List<ErrorCriticalityFactor>();
    }
}
