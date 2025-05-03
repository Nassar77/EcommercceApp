using EcommerceApp.Application.Mapping;
using EcommerceApp.Application.Services.Implemantions;
using EcommerceApp.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceApp.Application.DependencyInjection;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}