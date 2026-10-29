using IKP.Domain.Common.Base;
using IKP.Domain.Entities.App.HR.Lookup;

namespace IKP.Domain.Entities.App.HR.CompetencyMatrix
{
    public class CompetencyMatrixTarget : FullAudit
    {
        public Guid MatrixItemId { get; set; }
        public Guid PositionId { get; set; }
        public Guid? GradeId { get; set; }
        public decimal? ReferenceValue { get; set; }
        public string? ReferenceText { get; set; }

        public CompetencyMatrixItem MatrixItem { get; set; } = null!; 
        public Position Position { get; set; } = null!; 
        public Grade? Grade { get; set; }
    }
}
