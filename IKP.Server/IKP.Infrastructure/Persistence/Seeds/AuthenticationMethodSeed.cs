using IKP.Domain.Common.Auth;
using IKP.Domain.Entities.Auth.Lookup;
using IKP.Infrastructure.Persistence.Seeds.Records;
using IKP.Infrastructure.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Persistence.Seeds;

/// <summary>Seeds built-in authentication methods.</summary>
public static class AuthenticationMethodSeed
{
    private const string PathKey = "Auth.AuthenticationMethod";
    private const uint BaseIndex = 7;

    private static readonly Guid PasswordId = BaseIndex.GetGuidForLevel(0, 1);

    /// <summary>
    /// Adds built-in authentication methods to the EF model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthenticationMethodDefinition>()
            .HasData(new SeedRecord<AuthenticationMethodDefinition>(PasswordId, AuthenticationMethodCodes.Password).CreateDefinitionLocalized(PathKey));
    }
}
