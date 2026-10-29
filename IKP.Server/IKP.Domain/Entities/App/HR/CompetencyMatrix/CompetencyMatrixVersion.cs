using IKP.Domain.Common.Base;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyMatrixVersion : FullAudit
    {
        public Guid MatrixId { get; set; }
        public int VersionNumber { get; set; }
        public string? ChangeNotes { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsPublished { get; set; }

        public CompetencyMatrix Matrix { get; set; } = null!; 
        public ICollection<CompetencyMatrixSection> Sections { get; set; } = new List<CompetencyMatrixSection>();
    }
}
