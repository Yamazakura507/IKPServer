using IKP.Api.Infrastructure.DI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddDI();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UsesApp();
app.MapControllers();
app.Run();
