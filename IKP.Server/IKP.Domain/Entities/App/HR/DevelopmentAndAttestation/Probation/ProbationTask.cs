using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Probation
{
    public class ProbationTask : FullAudit, IHasOrder, IShortLookup
    {
        public Guid PlanId { get; set; }
        public int SortOrder { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? Deadline { get; set; }
        public decimal CompletionPercent { get; set; }
        public string? WeeklyReport { get; set; }
        public string? Comment { get; set; }

        public ProbationPlan Plan { get; set; } = null!;
    }
}
