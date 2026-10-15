using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using IKP.Api.Diagnostics;
using IKP.Api.Security;
using IKP.Application.Auth.Interfaces;
using IKP.Application.Auth.Models;
using IKP.Application.Common.Interfaces;
using IKP.Domain.Entities.Auth.Users;
using IKP.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

AuthOptions authOptions = builder.Configuration.GetSection("Authentication").Get<AuthOptions>() ?? new AuthOptions();
string signingKey = authOptions.SigningKey ?? throw new InvalidOperationException("Authentication:SigningKey must be configured outside source control.");

if (Encoding.UTF8.GetByteCount(signingKey) < 32) throw new InvalidOperationException("Authentication:SigningKey must contain at least 32 UTF-8 bytes.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = authOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = authOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("auth", httpContext => RateLimitPartition.GetFixedWindowLimiter(
        httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 30,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            AutoReplenishment = true
        }));
});

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IPasswordHasher<AuthUser>, PasswordHasher<AuthUser>>();
builder.Services.AddScoped<IDiagnosticRepository, DiagnosticRepository>();
builder.Services.AddScoped<IDiagnosticService, DiagnosticService>();
builder.Services.AddScoped<IDiagnosticAreaResolver, DiagnosticAreaResolver>();
builder.Services.AddScoped<IDiagnosticStatisticsService, DiagnosticStatisticsService>();
builder.Services.AddScoped<IDiagnosticCriticalityService, DiagnosticCriticalityService>();

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Authentication"));
builder.Services.Configure<DiagnosticCriticalityWorkerOptions>(builder.Configuration.GetSection("Diagnostics:Criticality:Worker"));
builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
builder.Services.Configure<DiagnosticStatisticsWorkerOptions>(builder.Configuration.GetSection("Diagnostics:Statistics:Worker"));

builder.Services.AddHostedService<DiagnosticStatisticsWorker>();
builder.Services.AddHostedService<DiagnosticCriticalityWorker>();

builder.Services.AddSingleton<ICurrentUserContext, HttpCurrentUserContext>();
builder.Services.AddSingleton<DiagnosticCriticalityCalculator>();
builder.Services.AddSingleton<IRedisDistributedLockService, RedisDistributedLockService>();
builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));
builder.Services.AddSingleton<IAuditValueSerializer, AuditValueSerializer>();
builder.Services.AddSingleton<IAuditActionRegistry, AuditActionRegistry>();
builder.Services.AddSingleton<IErrorFingerprintService, ErrorFingerprintService>();
builder.Services.AddSingleton<IExceptionDetailsExtractor, ExceptionDetailsExtractor>();
builder.Services.AddSingleton<ICorrelationIdAccessor, CorrelationIdAccessor>();
builder.Services.AddSingleton<AuditSaveChangesInterceptor>();

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

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCorrelationId();
app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
