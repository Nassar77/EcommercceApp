using EcommerceApp.Application.Services.Interfaces.Logging;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces;
using EcommerceApp.Infrastructure.Data;
using EcommerceApp.Infrastructure.Midlleware;
using EcommerceApp.Infrastructure.Reposatories;
using EcommerceApp.Infrastructure.Service;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceApp.Infrastructure.DependencyInjection;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = "Default";
        services.AddDbContext<AppDbContext>(option =>
        option.UseSqlServer(config.GetConnectionString(connectionString),
        sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName);
            sqlOptions.EnableRetryOnFailure();

        }).UseExceptionProcessor(),
        ServiceLifetime.Scoped);
        services.AddScoped<IGeneric<Product>, GenericReposatory<Product>>();
        services.AddScoped<IGeneric<Category>, GenericReposatory<Category>>();
        services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));
        return services;
    }
    public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMidlleware>();
        return app;
    }
}
