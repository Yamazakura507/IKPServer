using IKP.Domain.Common.Base;
using IKP.Domain.Common.Interfaces.Information;
using IKP.Domain.Common.Interfaces.Lookup;
using IKP.Domain.Entities.Diagnostics.Errors;

namespace IKP.Domain.Entities.Diagnostics.Areas
{
    public class DiagnosticArea : Entity, IShortLocalizedLookup, IHasCode, IHasActiveState
    {
        public string Code { get; set; } = null!;

        public string NameKey { get; set; } = null!;

        public string? DescriptionKey { get; set; }

        public decimal ImportanceScore { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ErrorGroup> ErrorGroups { get; set; } = new List<ErrorGroup>();
    }
}
