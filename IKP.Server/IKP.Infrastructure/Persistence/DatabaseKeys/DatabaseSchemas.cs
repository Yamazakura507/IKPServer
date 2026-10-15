namespace IKP.Infrastructure.Persistence.DatabaseKeys;

/// <summary>
/// Contains database schema names used by the application.
/// </summary>
public static class DatabaseSchemas
{
    private const string Diagnostics = "diagnostics";

    /// <summary>Gets the main application schema.</summary>
    public const string App = "app";

    /// <summary>Gets the authentication schema.</summary>
    public const string Auth = "auth";

    /// <summary>Gets the authentication lookup schema.</summary>
    public const string AuthLookup = "auth_lookup";

    /// <summary>Gets the audit log schema.</summary>
    public const string Logs = "app_logs";

    /// <summary>Gets the audit lookup schema.</summary>
    public const string LogsLookup = $"{Logs}_lookup";

    /// <summary>Gets the diagnostics lookup schema.</summary>
    public const string DiagnosticsLookup = $"{Diagnostics}_lookup";

    /// <summary>Gets the diagnostics core schema.</summary>
    public const string DiagnosticsCore = $"{Diagnostics}_core";

    /// <summary>Gets the diagnostics statistics schema.</summary>
    public const string DiagnosticsStatistics = $"{Diagnostics}_statistics";
}
