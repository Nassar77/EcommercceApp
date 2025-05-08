using EcommerceApp.Application.Services.Interfaces.Cart;
using EcommerceApp.Application.Services.Interfaces.Logging;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Entities.Identity;
using EcommerceApp.Domain.Interfaces;
using EcommerceApp.Domain.Interfaces.Authentication;
using EcommerceApp.Domain.Interfaces.Cart;
using EcommerceApp.Infrastructure.Data;
using EcommerceApp.Infrastructure.Midlleware;
using EcommerceApp.Infrastructure.Reposatories;
using EcommerceApp.Infrastructure.Reposatories.Authentication;
using EcommerceApp.Infrastructure.Reposatories.Cart;
using EcommerceApp.Infrastructure.Service;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
        

        services.AddDefaultIdentity<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.Tokens.EmailConfirmationTokenProvider=TokenOptions.DefaultEmailProvider;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 1;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
            };
        });

        services.AddScoped<IUserManagement, UserManagement>();
        services.AddScoped<ITokenManagement, TokenManagement>();
        services.AddScoped<IRoleManagement, RoleManagement>();
        services.AddScoped<IPaymentMethod, PaymentMethodReposatory>();
        services.AddScoped<IPaymentService, StripePaymentService>();
        services.AddScoped<ICart, CartReposatory>();

        Stripe.StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

        return services;
    }
    public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMidlleware>();
        return app;
    }
}
