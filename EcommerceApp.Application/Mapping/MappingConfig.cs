using AutoMapper;
using EcommerceApp.Application.DTOs.Cart;
using EcommerceApp.Application.DTOs.Category;
using EcommerceApp.Application.DTOs.Identity;
using EcommerceApp.Application.DTOs.Product;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Entities.Cart;
using EcommerceApp.Domain.Entities.Identity;

namespace EcommerceApp.Application.Mapping;
public class MappingConfig:Profile
{
    public MappingConfig()
    {
        CreateMap<CreateCategory, Category>();
        CreateMap<CreateProduct, Product>();

        CreateMap<Category, GetCategory>();
        CreateMap<Product, GetProduct>();
        
        CreateMap<CreateUser, AppUser>();
        CreateMap<LoginUser, AppUser>();

        CreateMap<PaymentMethod, GetPaymentMethod>();
        CreateMap<CreateAchieve, Achieve>();
    }
}
