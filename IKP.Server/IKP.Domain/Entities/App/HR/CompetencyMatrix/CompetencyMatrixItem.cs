using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyMatrixItem : FullAudit, IShortLookup, IHasOrder
    {
        public Guid SectionId { get; set; }
        public Guid? ParentItemId { get; set; }
        public string Name { get; set; } = null!; 
        public string? Description { get; set; }
        public int SortOrder { get; set; }

        public CompetencyMatrixSection Section { get; set; } = null!; 
        public CompetencyMatrixItem? ParentItem { get; set; }
        public ICollection<CompetencyMatrixItem> Children { get; set; } = new List<CompetencyMatrixItem>(); 
        public ICollection<CompetencyMatrixTarget> Targets { get; set; } = new List<CompetencyMatrixTarget>();
    }
}
