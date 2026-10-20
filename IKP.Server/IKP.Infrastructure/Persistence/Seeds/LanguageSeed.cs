using IKP.Domain.Common.Localization;
using IKP.Domain.Entities.Localization;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>Seeds the built-in English and Russian languages.</summary>
public static class LanguageSeed
{
    private const string PathKey = "Localization.Language";
    private const uint BaseIndex = 1;

    private static readonly Guid EnglishId = BaseIndex.GetGuidForLevel(0, 1);
    private static readonly Guid RussianId = BaseIndex.GetGuidForLevel(0, 2);

    /// <summary>Adds the built-in languages to the EF model.</summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>().HasData(
            new SeedRecord<Language>(EnglishId, LanguageCodes.English, SortOrder: 10, Configure: x =>
            {
                x.CultureCode = "en-US";
                x.NativeName = "English";
                x.IsDefault = true;
                x.IsRightToLeft = false;
            }).CreateDefinitionLocalized(PathKey),
            new SeedRecord<Language>(RussianId, LanguageCodes.Russian, SortOrder: 20, Configure: x =>
            {
                x.CultureCode = "ru-RU";
                x.NativeName = "Русский";
                x.IsDefault = false;
                x.IsRightToLeft = false;
            }).CreateDefinitionLocalized(PathKey));
    }
}
