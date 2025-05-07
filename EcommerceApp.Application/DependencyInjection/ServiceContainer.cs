using EcommerceApp.Application.Mapping;
using EcommerceApp.Application.Services.Implemantions;
using EcommerceApp.Application.Services.Implemantions.Authentication;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Application.Services.Interfaces.Authentication;
using EcommerceApp.Application.Validations;
using EcommerceApp.Application.Validations.Authontication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceApp.Application.DependencyInjection;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingConfig));
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}