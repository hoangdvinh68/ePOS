using ePOS.API;
using ePOS.Infrastructure;
using ePOS.Infrastructure.Persistence.Migrates;
using ePOS.Shared;
using ePOS.Shared.ValueObjects;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var appSettings = new AppSettings();
new ConfigurationBuilder()
    .SetBasePath(environment.ContentRootPath)
    .AddJsonFile(Path.Combine(nameof(AppSettings), $"appsettings.{environment.EnvironmentName}.json"))
    .AddEnvironmentVariables()
    .Build().Bind(appSettings);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var services = builder.Services;
services.AddSingleton(appSettings);
services.AddAPIServices(appSettings);
services.AddSharedServices(appSettings);
services.AddInfrastructureServices(appSettings);

var app = builder.Build();
app.UseHealthChecks("/health");
app.UseMiddleware<ExceptionHandlerMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MigrateDatabase();
app.MapControllers();
app.Run();