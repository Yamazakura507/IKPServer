using IKP.Domain.Common.Updates;
using IKP.Domain.Entities.Updates.Lookups;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;
namespace IKP.Infrastructure.Persistence.Seeds;
public static class UpdateSeed
{
    private const string PathCategoryKeys = "Updates.Categories";
    private const string PathPlatformKeys = "Updates.Platforms";
    private const string PathProductKeys = "Updates.Products";
    private const uint BaseIndex = 1;

    internal static readonly Guid ClientsId = BaseIndex.GetGuidForLevel(1, 1);
    internal static readonly Guid LibrariesId = BaseIndex.GetGuidForLevel(1, 2);
    internal static readonly Guid InstallersId = BaseIndex.GetGuidForLevel(1, 3);


    internal static readonly Guid WindowsId = BaseIndex.GetGuidForLevel(1, 1);
    internal static readonly Guid AndroidId = BaseIndex.GetGuidForLevel(1, 2);

    internal static readonly IReadOnlyDictionary<Guid, (IReadOnlyList<(Guid id, string code)> products, string path)> ProductIds = 
        new Dictionary<Guid, (IReadOnlyList<(Guid, string)>, string)>
        {
            { 
                ClientsId, 
                ([
                    (BaseIndex.GetGuidForLevel(1, 1), UpdatesProductCodes.Developer), 
                    (BaseIndex.GetGuidForLevel(1, 2), UpdatesProductCodes.HRBase), 
                    (BaseIndex.GetGuidForLevel(1, 3), UpdatesProductCodes.HRBoss)
                ], $"{PathProductKeys}.Client")
            },
            {
                LibrariesId, 
                ([
                    (BaseIndex.GetGuidForLevel(2, 1), UpdatesProductCodes.ThemeManager),
                    (BaseIndex.GetGuidForLevel(2, 2), UpdatesProductCodes.FileManger),
                    (BaseIndex.GetGuidForLevel(2, 3), UpdatesProductCodes.ReportManager),
                    (BaseIndex.GetGuidForLevel(2, 4), UpdatesProductCodes.MessageManager),
                    (BaseIndex.GetGuidForLevel(2, 5), UpdatesProductCodes.NoticeManager)
                ], $"{PathProductKeys}.Lib")
            },
            { 
                InstallersId, 
                ([ 
                    (BaseIndex.GetGuidForLevel(3, 1), UpdatesProductCodes.IKPSoft) 
                ], $"{PathProductKeys}.Installer") 
            }
        };

    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRecord<UpdateCategory>[] categories =
        {
            new (ClientsId, UpdatesCategoryCodes.Clients),
            new (LibrariesId, UpdatesCategoryCodes.Libs),
            new (InstallersId, UpdatesCategoryCodes.Installers)
        };
        SeedRecord<UpdatePlatform>[] platforms =
        {
            new (WindowsId, UpdatesPlatformCodes.Windows),
            new (AndroidId, UpdatesPlatformCodes.Android)
        };
        var products = ProductIds
            .SelectMany(kvp => kvp.Value.products.Select(p => (
                Path: kvp.Value.path,
                Seeds: new SeedRecord<UpdateProduct>(p.id, p.code, Configure: x => { x.CategoryId = kvp.Key; x.PlatformId = WindowsId; })
            )));


        modelBuilder.Entity<UpdateCategory>().HasData(categories.CreateListDefinitionLocalized(PathCategoryKeys));
        modelBuilder.Entity<UpdatePlatform>().HasData(platforms.CreateListDefinitionLocalized(PathPlatformKeys));
        modelBuilder.Entity<UpdateProduct>().HasData(products.Select(i => i.Seeds.CreateDefinitionLocalized(i.Path)));
    }
}
