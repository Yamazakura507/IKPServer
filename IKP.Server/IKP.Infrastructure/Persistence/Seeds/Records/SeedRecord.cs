using IKP.Infrastructure.Persistence.Seeds.Interfaces;

namespace IKP.Infrastructure.Persistence.Seeds.Records
{
    internal record SeedRecord<T>(Guid Id, string Code, string? NameKey = null, int? SortOrder = null, string? DescriptionKey = null, Guid? FileId = null, Action<T>? Configure = null) : ISeed<T>
    {
    }
}
