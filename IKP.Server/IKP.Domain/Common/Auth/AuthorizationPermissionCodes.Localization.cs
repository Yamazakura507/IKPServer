namespace IKP.Domain.Common.Auth;

/// <summary>
/// Contains localization authorization permission codes.
/// </summary>
public static partial class AuthorizationPermissionCodes
{
    /// <summary>Allows reading supported languages and language package metadata.</summary>
    public const string LocalizationRead = "localization.read";

    /// <summary>Allows managing languages and language packages.</summary>
    public const string LocalizationManage = "localization.manage";
}
