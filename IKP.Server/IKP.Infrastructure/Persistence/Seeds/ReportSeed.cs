using IKP.Domain.Common.Reports;
using IKP.Domain.Entities.Reports.Lookups;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>
/// Seeds built-in report categories.
/// </summary>
public static class ReportSeed
{
    private const string PathKey = "Reports";
    private const string PathCategoriesKey = $"{PathKey}.Categories";
    private const string PathExecutionStatusKey = $"{PathKey}.ExecutionStatus";
    private const string PathDataTypeKey = $"{PathKey}.DataType";
    private const uint BaseIndex = 9;

    private static readonly Guid FirsId = BaseIndex.GetGuidForLevel(1, 1);
    private static readonly Guid SecondId = BaseIndex.GetGuidForLevel(1, 2);
    private static readonly Guid ThirdId = BaseIndex.GetGuidForLevel(1, 3);
    private static readonly Guid FourthId = BaseIndex.GetGuidForLevel(1, 4);
    private static readonly Guid FifthId = BaseIndex.GetGuidForLevel(1, 5);
    private static readonly Guid SixthId = BaseIndex.GetGuidForLevel(1, 6);
    private static readonly Guid SeventhId = BaseIndex.GetGuidForLevel(1, 7);

    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRecord<ReportCategory>[] categories =
        {
            new(FirsId, ReportCategoryCodes.Operational),
            new(SecondId, ReportCategoryCodes.HumanResources),
            new(ThirdId, ReportCategoryCodes.Diagnostics),
            new(FourthId, ReportCategoryCodes.Administration)
        };

        SeedRecord<ReportExecutionStatus>[] executionStatus = 
        {
            new(FirsId, ReportExecutionStatusCodes.Queued),
            new(SecondId, ReportExecutionStatusCodes.Running),
            new(ThirdId, ReportExecutionStatusCodes.Completed),
            new(FourthId, ReportExecutionStatusCodes.Failed),
            new(FifthId, ReportExecutionStatusCodes.Cancelled)
        };

        SeedRecord<ReportDataType>[] reportDataTypes =
        {
            new(FirsId, ReportParameterDataTypeCodes.String),
            new(SecondId, ReportParameterDataTypeCodes.Integer),
            new(ThirdId, ReportParameterDataTypeCodes.Decimal),
            new(FourthId, ReportParameterDataTypeCodes.Boolean),
            new(FifthId, ReportParameterDataTypeCodes.Date),
            new(SixthId, ReportParameterDataTypeCodes.DateTime),
            new(SeventhId, ReportParameterDataTypeCodes.Guid)
        };

        modelBuilder.Entity<ReportCategory>().HasData(categories.CreateListDefinitionLocalized(PathCategoriesKey));
        modelBuilder.Entity<ReportExecutionStatus>().HasData(executionStatus.CreateListDefinitionLocalized(PathExecutionStatusKey));
        modelBuilder.Entity<ReportDataType>().HasData(reportDataTypes.CreateListDefinitionLocalized(PathDataTypeKey));
    }
}
