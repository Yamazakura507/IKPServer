using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Diagnostics.Definitions;

namespace IKP.Domain.Entities.Diagnostics.Criticality
{
    public class ErrorCriticalityFactor : Entity
    {
        public Guid ErrorCriticalityId { get; set; }

        public Guid DefinitionId { get; set; }

        public decimal Score { get; set; }

        public decimal Weight { get; set; }

        public decimal Contribution { get; set; }

        public decimal? Value { get; set; }

        public DateTime CalculatedAt { get; set; }

        public DiagnosticDefinition Definition { get; set; } = null!;

        public ErrorCriticality ErrorCriticality { get; set; } = null!;
    }
}
