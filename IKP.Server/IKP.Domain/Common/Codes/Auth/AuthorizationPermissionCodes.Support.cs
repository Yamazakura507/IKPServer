namespace IKP.Domain.Common.Auth;

/// <summary>Contains support authorization permission codes.</summary>
public static partial class AuthorizationPermissionCodes
{
    /// <summary>Allows viewing support tickets and public messages.</summary>
    public const string SupportRead = "support.read";
    /// <summary>Allows creating support tickets.</summary>
    public const string SupportCreate = "support.create";
    /// <summary>Allows updating ticket metadata.</summary>
    public const string SupportUpdate = "support.update";
    /// <summary>Allows assigning support tickets.</summary>
    public const string SupportAssign = "support.assign";
    /// <summary>Allows changing support ticket status.</summary>
    public const string SupportStatusManage = "support.status.manage";
    /// <summary>Allows deleting support tickets.</summary>
    public const string SupportDelete = "support.delete";
    /// <summary>Allows adding public support messages.</summary>
    public const string SupportMessagesCreate = "support.messages.create";
    /// <summary>Allows reading internal support notes.</summary>
    public const string SupportInternalNotesRead = "support.internal_notes.read";
    /// <summary>Allows creating internal support notes.</summary>
    public const string SupportInternalNotesCreate = "support.internal_notes.create";
}
