#region usings
using IKP.Api.Authorization;
using IKP.Api.Diagnostics;
using IKP.Api.Diagnostics.Correlation;
using IKP.Api.Diagnostics.Correlation.Interfaces;
using IKP.Api.Security;
using IKP.Application.Auth.Administration.Interfaces;
using IKP.Application.Auth.Interfaces;
using IKP.Application.Auth.Models;
using IKP.Application.Common.Interfaces;
using IKP.Application.Diagnostics.Criticality.Interfaces;
using IKP.Application.Diagnostics.Criticality.Models;
using IKP.Application.Diagnostics.Criticality.Services;
using IKP.Application.Diagnostics.Exceptions;
using IKP.Application.Diagnostics.Exceptions.Interfaces;
using IKP.Application.Diagnostics.Fingerprinting;
using IKP.Application.Diagnostics.Fingerprinting.Interfaces;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Resolvers;
using IKP.Application.Diagnostics.Resolvers.Interfaces;
using IKP.Application.Diagnostics.Services;
using IKP.Application.Diagnostics.Statistics.Interfaces;
using IKP.Application.Diagnostics.Statistics.Models;
using IKP.Application.Diagnostics.Statistics.Services;
using IKP.Application.Files.Interfaces;
using IKP.Application.Files.Models;
using IKP.Domain.Entities.Auth.Users;
using IKP.Infrastructure.Interceptors;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Services.Audit;
using IKP.Infrastructure.Services.Audit.Interfaces;
using IKP.Infrastructure.Services.Auth;
using IKP.Infrastructure.Services.Auth.Administration;
using IKP.Infrastructure.Services.Diagnostics;
using IKP.Infrastructure.Services.Files;
using IKP.Infrastructure.Services.Redis;
using IKP.Infrastructure.Services.Redis.Interfaces;
using IKP.Infrastructure.Services.Support;
using IKP.Application.Support.Interfaces;
using IKP.Application.System.Interfaces;
using IKP.Application.Updates.Interfaces;
using IKP.Application.Reports.Interfaces;
using IKP.Infrastructure.Services.Updates;
using IKP.Infrastructure.Services.Reports;
using IKP.Infrastructure.Services.System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using IKP.Infrastructure.Persistence.DatabaseKeys;
using Microsoft.EntityFrameworkCore;
#endregion

namespace IKP.Api.Infrastructure.DI
{
    internal static class DIExtensions
    {
        internal static string? ConectionString { get; private set; }

        internal const string MigrationHistory = "__EFMigrationsHistory";

        internal static void AddDI(this WebApplicationBuilder builder)
        {
            ConectionString = builder.GetConectionString();

            builder.AddContext(ConectionString);
            builder.AddSingletons(ConectionString);
            builder.AddScopeds();
            builder.AddConfigures();
            builder.AddHostedService();
            builder.AddOtherServices();
            builder.AddFileStorageLimits();
        }

        internal static void UsesApp(this WebApplication app)
        {
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseRouting();
            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private static void AddSingletons(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));
            builder.Services.AddSingleton<IFileStorageProvider, LocalFileStorageProvider>();
            builder.AddSingletonsAudit();
            builder.AddSingletonsDiagnostics();
            builder.AddSingletoneAuth();
        }

        private static void AddScopeds(this WebApplicationBuilder builder)
        {
            builder.AddScopedDiagnostics();
            builder.AddScopedAuth();
            builder.AddScopedUpdate();
            builder.AddScopedReport();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<ISupportService, SupportService>();
            builder.Services.AddScoped<IBackgroundJobService, BackgroundJobService>();
        }

        private static void AddSingletonsAudit(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAuditValueSerializer, AuditValueSerializer>();
            builder.Services.AddSingleton<IAuditActionRegistry, AuditActionRegistry>();
            builder.Services.AddSingleton<AuditSaveChangesInterceptor>();
        }

        private static void AddSingletonsDiagnostics(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IErrorFingerprintService, ErrorFingerprintService>();
            builder.Services.AddSingleton<IExceptionDetailsExtractor, ExceptionDetailsExtractor>();
            builder.Services.AddSingleton<ICorrelationIdAccessor, CorrelationIdAccessor>();
            builder.Services.AddSingleton<IRedisDistributedLockService, RedisDistributedLockService>();
            builder.Services.AddSingleton<DiagnosticCriticalityCalculator>();
        }

        private static void AddSingletoneAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ICurrentUserContext, HttpCurrentUserContext>();
            builder.Services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            builder.Services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, PermissionAuthorizationHandler>();
        }

        private static void AddScopedDiagnostics(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDiagnosticRepository, DiagnosticRepository>();
            builder.Services.AddScoped<IDiagnosticService, DiagnosticService>();
            builder.Services.AddScoped<IDiagnosticAreaResolver, DiagnosticAreaResolver>();
            builder.Services.AddScoped<IDiagnosticStatisticsService, DiagnosticStatisticsService>();
            builder.Services.AddScoped<IDiagnosticCriticalityService, DiagnosticCriticalityService>();
        }

        private static void AddScopedAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            builder.Services.AddScoped<IUserAdministrationService, UserAdministrationService>();
            builder.Services.AddScoped<IPasswordHasher<AuthUser>, PasswordHasher<AuthUser>>();
        }

        private static void AddScopedUpdate(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUpdateProductService, UpdateProductService>();
            builder.Services.AddScoped<IUpdateVersionService, UpdateVersionService>();
            builder.Services.AddScoped<IUpdateReleaseNoteService, UpdateReleaseNoteService>();
            builder.Services.AddScoped<IUpdateProductAccessService, UpdateProductAccessService>();
        }

        private static void AddScopedReport(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IReportDefinitionService, ReportDefinitionService>();
            builder.Services.AddScoped<IReportTemplateService, ReportTemplateService>();
            builder.Services.AddScoped<IReportParameterService, ReportParameterService>();
        }

        private static void AddConfigures(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Authentication"));
            builder.Services.Configure<DiagnosticCriticalityWorkerOptions>(builder.Configuration.GetSection("Diagnostics:Criticality:Worker"));
            builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
            builder.Services.Configure<DiagnosticStatisticsWorkerOptions>(builder.Configuration.GetSection("Diagnostics:Statistics:Worker"));
            builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("Files:Storage"));
        }

        private static void AddHostedService(this WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<DiagnosticStatisticsWorker>();
            builder.Services.AddHostedService<DiagnosticCriticalityWorker>();
            builder.Services.AddHostedService<FileUploadCleanupWorker>();
        }

        private static void AddFileStorageLimits(this WebApplicationBuilder builder)
        {
            FileStorageOptions options = builder.Configuration.GetSection("Files:Storage").Get<FileStorageOptions>() ?? new FileStorageOptions();

            if (options.MaximumFileSizeBytes <= 0) throw new InvalidOperationException("Files:Storage:MaximumFileSizeBytes must be greater than zero.");
            if (options.CopyBufferSizeBytes < 64 * 1024) throw new InvalidOperationException("Files:Storage:CopyBufferSizeBytes must be at least 65536 bytes.");

            builder.WebHost.ConfigureKestrel(serverOptions => serverOptions.Limits.MaxRequestBodySize = options.MaximumFileSizeBytes);
        }

        private static void AddOtherServices(this WebApplicationBuilder builder)
        {
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

            builder.AddOtherAuthServices();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.AddOpenApi();
        }

        private static void AddOtherAuthServices(this WebApplicationBuilder builder)
        {
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

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthorization();
        }

        private static void AddContext(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddDbContextFactory<AppDbContext>(
            (serviceProvider, options) =>
            {
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(MigrationHistory, DatabaseSchemas.App));

                options.AddInterceptors(serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>());
            });
        }

        private static string GetConectionString(this WebApplicationBuilder builder) 
            => builder.Configuration.GetConnectionString(DatabaseConnections.Default) ?? throw new InvalidOperationException("Database connection string 'Default' is not configured.");
    }
}
