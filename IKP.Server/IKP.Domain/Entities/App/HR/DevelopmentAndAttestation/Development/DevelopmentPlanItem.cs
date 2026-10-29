using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development
{
    public class DevelopmentPlanItem : FullAudit, IHasOrder
    {
        public Guid PlanId { get; set; }
        public Guid ItemTypeId { get; set; }
        public Guid DevelopmentNeedId { get; set; }
        public Guid? ControlMethodId { get; set; }
        public Guid? DevelopmentMethodId { get; set; }
        
        public int SortOrder { get; set; }
        public string? Goal { get; set; }
        public DateOnly? Deadline { get; set; }
        public ICollection<string>? Actions { get; set; }
        public decimal? CompletionPercent { get; set; }
        public string? Comments { get; set; }

        public DevelopmentItemType ItemType { get; set; } = null!;
        public DevelopmentPlan Plan { get; set; } = null!;
        public DevelopmentNeeds DevelopmentNeed { get; set; } = null!;
        public DevelopmentControlMethods? DevelopmentControlMethod { get; set; }
        public DevelopmentMethods? DevelopmentMethod { get; set; }

        public ICollection<ResponsibleEmployeesOfPlanItem> ResponsibleEmployees { get; set; } = new List<ResponsibleEmployeesOfPlanItem>();
    }
}
