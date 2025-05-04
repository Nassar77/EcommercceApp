using EcommerceApp.Infrastructure.DependencyInjection;
using EcommerceApp.Application.DependencyInjection;
using Scalar.AspNetCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
Log.Logger.Information("application is building........");


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();

try
{
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }
    app.UseInfrastructureService();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    Log.Logger.Information("Application is running.......");

    app.Run();
}catch(Exception ex)
{
    Log.Logger.Error(ex, "Application faild to start.....");
}
finally
{
    Log.CloseAndFlush();
}