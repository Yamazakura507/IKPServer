namespace IKP.Application.Auth.Administration.Models;

/// <summary>Requests activation or deactivation of an authentication user.</summary>
public sealed class SetUserStatusRequest
{
    /// <summary>Gets or sets whether the account is active.</summary>
    public bool IsActive { get; set; }
}
