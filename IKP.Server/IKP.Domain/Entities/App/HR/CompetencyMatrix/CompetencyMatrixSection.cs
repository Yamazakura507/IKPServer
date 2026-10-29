using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyMatrixSection : FullAudit, IHasName, IHasOrder
    {
        public Guid MatrixVersionId { get; set; }
        public Guid SectionTypeId { get; set; }
        public string Name { get; set; } = null!; 
        public int SortOrder { get; set; }

        public CompetencySectionType SectionType { get; set; } = null!;
        public CompetencyMatrixVersion MatrixVersion { get; set; } = null!; 
        public ICollection<CompetencyMatrixItem> Items { get; set; } = new List<CompetencyMatrixItem>();
    }
}
