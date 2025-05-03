using AutoMapper;
using EcommerceApp.Application.DTOs.Category;
using EcommerceApp.Application.DTOs.Product;
using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Application.Mapping;
public class MappingConfig:Profile
{
    public MappingConfig()
    {
        CreateMap<CreateCategory, Category>();
        CreateMap<CreateProduct, Product>();

        CreateMap<Category, GetCategory>();
        CreateMap<Product, GetProduct>();
    }
}
