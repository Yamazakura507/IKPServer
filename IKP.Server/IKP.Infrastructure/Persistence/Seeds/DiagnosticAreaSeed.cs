using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Areas;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds
{
    /// <summary>
    /// Provides seed data for system diagnostic areas.
    /// </summary>
    public static class DiagnosticAreaSeed
    {
        private const string pathKey = "Diagnostics.Area";
        private const uint baseIndex = 3;

        private static readonly Guid ApiId = baseIndex.GetGuidForLevel(0, 1);
        private static readonly Guid DiagnosticsId = baseIndex.GetGuidForLevel(0, 2);
        private static readonly Guid EncryptionId = baseIndex.GetGuidForLevel(0, 3);
        private static readonly Guid AuthenticationId = baseIndex.GetGuidForLevel(0, 4);
        private static readonly Guid AuthorizationId = baseIndex.GetGuidForLevel(0, 5);
        private static readonly Guid RegistrationId = baseIndex.GetGuidForLevel(0, 6);
        private static readonly Guid SupportId = baseIndex.GetGuidForLevel(0, 7);

        /// <summary>
        /// Adds system diagnostic areas to the EF Core model.
        /// </summary>
        /// <param name="modelBuilder">The EF Core model builder.</param>
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosticArea>().HasData(
                new[]
                {
                    new SeedRecord<DiagnosticArea>(ApiId, DiagnosticAreaCodes.Api, Configure: entity => entity.ImportanceScore = 90),
                    new SeedRecord<DiagnosticArea>(DiagnosticsId, DiagnosticAreaCodes.Diagnostics, Configure: entity => entity.ImportanceScore = 80),
                    new SeedRecord<DiagnosticArea>(EncryptionId, DiagnosticAreaCodes.Encryption, Configure: entity => entity.ImportanceScore = 95),
                    new SeedRecord<DiagnosticArea>(AuthenticationId, DiagnosticAreaCodes.Authentication, Configure: entity => entity.ImportanceScore = 100),
                    new SeedRecord<DiagnosticArea>(AuthorizationId, DiagnosticAreaCodes.Authorization, Configure: entity => entity.ImportanceScore = 100),
                    new SeedRecord<DiagnosticArea>(RegistrationId, DiagnosticAreaCodes.Registration, Configure: entity => entity.ImportanceScore = 90),
                    new SeedRecord<DiagnosticArea>(SupportId, DiagnosticAreaCodes.Support, Configure: entity => entity.ImportanceScore = 70)
                }.CreateListDefinitionLocalized(pathKey)
            );
        }
    }
}
