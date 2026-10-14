namespace IKP.Infrastructure.Persistence.Seeds.Interfaces
{
    internal interface ISeed<T>
    {
        Guid Id { get; }
        string Code { get; }
        string? NameKey { get; } 
        int? SortOrder { get; }
        string? DescriptionKey { get; }
        Guid? FileId { get; } 
        Action<T>? Configure { get; }
    }
}
