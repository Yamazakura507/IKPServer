using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.App.HR.DevelopmentAndAttestation.Probation
{
    public class ProbationPlan : FullAudit
    {
        public Guid EmployeeId { get; set; }
        public Guid ManagerEmployeeId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Notes { get; set; }
        public decimal? FinalCompletionPercent { get; set; }
        public string? FinalComment { get; set; }

        public Employee Employee { get; set; } = null!; 
        public Employee ManagerEmployee { get; set; } = null!; 
        public ICollection<ProbationTask> Tasks { get; set; } = new List<ProbationTask>();
    }
}
