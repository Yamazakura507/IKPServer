using IKP.Domain.Common.Base;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development
{
    public class DevelopmentPlan : FullAudit
    {
        public Guid EmployeeId { get; set; }
        public Guid ManagerEmployeeId { get; set; }
        public Guid TypeId { get; set; }
        public DateOnly PlanningDate { get; set; }
        public DateOnly? PeriodStart { get; set; }
        public DateOnly? PeriodEnd { get; set; }
        public string? Notes { get; set; }
        public decimal? ManagerEffectiveness { get; set; }
        public decimal? EmployeeEffectiveness { get; set; }


        public DevelopmentPlanType Type { get; set; } = null!;
        public Employee Employee { get; set; } = null!; 
        public Employee ManagerEmployee { get; set; } = null!; 
        public ICollection<DevelopmentPlanItem> Items { get; set; } = new List<DevelopmentPlanItem>();
    }
}
