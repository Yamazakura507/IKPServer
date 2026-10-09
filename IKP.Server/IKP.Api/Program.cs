using IKP.Application.Diagnostics.Fingerprinting;
using IKP.Application.Diagnostics.Fingerprinting.Interfaces;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Models;
using IKP.Application.Diagnostics.Services;
using IKP.Infrastructure.Interceptors;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using IKP.Infrastructure.Services.Audit;
using IKP.Infrastructure.Services.Audit.Interfaces;
using IKP.Infrastructure.Services.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration.GetConnectionString(DatabaseConnections.Default) ?? throw new InvalidOperationException("Database connection string 'Default' is not configured.");

string migrationHistory = "__EFMigrationsHistory";

builder.Services.AddScoped<IDiagnosticRepository, DiagnosticRepository>();
builder.Services.AddScoped<IDiagnosticService, DiagnosticService>();

builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));

builder.Services.AddSingleton<IAuditValueSerializer, AuditValueSerializer>();
builder.Services.AddSingleton<IAuditActionRegistry, AuditActionRegistry>();
builder.Services.AddSingleton<IErrorFingerprintService, ErrorFingerprintService>();
builder.Services.AddSingleton<AuditSaveChangesInterceptor>();

builder.Services.AddDbContextFactory<AppDbContext>(
    (serviceProvider, options) =>
    {
        options.UseNpgsql(
            connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(migrationHistory, DatabaseSchemas.App));

        options.AddInterceptors(serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>());
    });

builder.Services.AddControllers();
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost(
    "/api/diagnostics/test",
    async (
        IDiagnosticService diagnosticService,
        CancellationToken cancellationToken) =>
    {
        try
        {
            throw new InvalidOperationException("Diagnostic test exception.");
        }
        catch (Exception exception)
        {
            Guid errorGroupId = await diagnosticService.RegisterErrorAsync(
                exception,
                new DiagnosticErrorContext
                {
                    ApplicationId = "IKP.Api",
                    ApplicationVersion = "0.1.0",
                    Platform = "Server",
                    ErrorCode = "DIAGNOSTIC_TEST",
                    Context = new
                    {
                        Test = true,
                        CreatedAt = DateTime.UtcNow
                    }
                },
                cancellationToken);

            return Results.Ok(new
            {
                ErrorGroupId = errorGroupId
            });
        }
    });

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
