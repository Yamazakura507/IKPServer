using Microsoft.EntityFrameworkCore;
using Npgsql;
using IKP.Infrastructure.Interceptors;
using IKP.Infrastructure.Persistence;
using IKP.Infrastructure.Services.Audit;
using IKP.Infrastructure.Services.Audit.Interfaces;
using IKP.Infrastructure.Persistence.DatabaseKeys;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration.GetConnectionString(DatabaseConnections.Default) ?? throw new InvalidOperationException("Database connection string 'Default' is not configured.");

string migrationHistory = "__EFMigrationsHistory";

builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));

builder.Services.AddSingleton<IAuditValueSerializer, AuditValueSerializer>();
builder.Services.AddSingleton<IAuditActionRegistry, AuditActionRegistry>();
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
