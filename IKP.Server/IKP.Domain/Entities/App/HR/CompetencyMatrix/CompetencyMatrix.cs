using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyMatrix : FullAudit, IHasName, IHasActiveState
    {
        public string Name { get; set; } = null!; 
        public bool IsActive { get; set; } = true;

        public ICollection<CompetencyMatrixVersion> Versions { get; set; } = new List<CompetencyMatrixVersion>();
    }
}
