using IKP.Domain.Common.Base;
using IKP.Domain.Entities.Auth.Credentials;

namespace IKP.Domain.Entities.Auth.Lookup;

/// <summary>
/// Defines an authentication method available to the server.
/// </summary>
public class AuthenticationMethodDefinition : FullLookupLocalized
{
    /// <summary>
    /// Gets credentials that use this authentication method.
    /// </summary>
    public ICollection<UserCredential> Credentials { get; set; } = new List<UserCredential>();
}
