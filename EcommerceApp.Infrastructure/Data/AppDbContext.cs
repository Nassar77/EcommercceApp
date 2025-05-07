using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Entities.Cart;
using EcommerceApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Infrastructure.Data;
public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PaymentMethod> paymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Id = "9eaa03df-8e4f-4161-85de-0f6e5e30bfd4",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
             new IdentityRole
             {
                 Id = "92b75286-d8f8-4061-9995-e6e23ccdee94",
                 Name = "User",
                 NormalizedName = "USER"
             });

        builder.Entity<PaymentMethod>()
            .HasData(
            new PaymentMethod
            {
                Id =Guid.NewGuid(),
                Name = "Credit Card",
            });
    }
}
