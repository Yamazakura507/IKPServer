using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Development
{
    public class ResponsibleEmployeesOfPlanItem : FullAudit
    {
        public Guid PlanItemId { get; set; }
        public Guid ResponsibleEmployeeId { get; set; }

        public DevelopmentPlanItem PlanItem { get; set; } = null!;
        public Employee ResponsibleEmployee { get; set; } = null!;
    }
}
