using IKP.Api.Diagnostics;
using IKP.Api.Diagnostics.Correlation;
using IKP.Api.Diagnostics.Correlation.Interfaces;
using IKP.Application.Diagnostics.Exceptions;
using IKP.Application.Diagnostics.Exceptions.Interfaces;
using IKP.Application.Diagnostics.Criticality.Interfaces;
using IKP.Application.Diagnostics.Criticality.Models;
using IKP.Application.Diagnostics.Criticality.Services;
using IKP.Application.Diagnostics.Fingerprinting;
using IKP.Application.Diagnostics.Fingerprinting.Interfaces;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Services;
using IKP.Application.Diagnostics.Resolvers;
using IKP.Application.Diagnostics.Resolvers.Interfaces;
using IKP.Application.Diagnostics.Statistics.Interfaces;
using IKP.Application.Diagnostics.Statistics.Models;
using IKP.Application.Diagnostics.Statistics.Services;
using IKP.Infrastructure.Interceptors;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using IKP.Infrastructure.Services.Audit;
using IKP.Infrastructure.Services.Audit.Interfaces;
using IKP.Infrastructure.Services.Diagnostics;
using IKP.Infrastructure.Services.Redis;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using IKP.Infrastructure.Services.Redis.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration.GetConnectionString(DatabaseConnections.Default) ?? throw new InvalidOperationException("Database connection string 'Default' is not configured.");

string migrationHistory = "__EFMigrationsHistory";

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IDiagnosticRepository, DiagnosticRepository>();
builder.Services.AddScoped<IDiagnosticService, DiagnosticService>();
builder.Services.AddScoped<IDiagnosticAreaResolver, DiagnosticAreaResolver>();
builder.Services.AddScoped<IDiagnosticStatisticsService, DiagnosticStatisticsService>();
builder.Services.AddScoped<IDiagnosticCriticalityService, DiagnosticCriticalityService>();

builder.Services.AddSingleton<DiagnosticCriticalityCalculator>();
builder.Services.AddSingleton<IRedisDistributedLockService, RedisDistributedLockService>();
builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));
builder.Services.AddSingleton<IAuditValueSerializer, AuditValueSerializer>();
builder.Services.AddSingleton<IAuditActionRegistry, AuditActionRegistry>();
builder.Services.AddSingleton<IErrorFingerprintService, ErrorFingerprintService>();
builder.Services.AddSingleton<IExceptionDetailsExtractor, ExceptionDetailsExtractor>();
builder.Services.AddSingleton<ICorrelationIdAccessor, CorrelationIdAccessor>();
builder.Services.AddSingleton<AuditSaveChangesInterceptor>();

builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
builder.Services.Configure<DiagnosticCriticalityWorkerOptions>(builder.Configuration.GetSection("Diagnostics:Criticality:Worker"));
builder.Services.Configure<DiagnosticStatisticsWorkerOptions>(builder.Configuration.GetSection("Diagnostics:Statistics:Worker"));

builder.Services.AddHostedService<DiagnosticStatisticsWorker>();
builder.Services.AddHostedService<DiagnosticCriticalityWorker>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCorrelationId();
app.UseAuthorization();

app.MapControllers();

app.MapGet(
    "/api/diagnostics/test-unhandled",
    () =>
    {
        throw new InvalidOperationException("Unhandled diagnostic test exception.");
    });

app.Run();
