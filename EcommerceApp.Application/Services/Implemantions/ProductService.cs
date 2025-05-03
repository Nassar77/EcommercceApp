using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.DTOs.Product;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces;

namespace EcommerceApp.Application.Services.Implemantions;
public class ProductService(IGeneric<Product> productInterface, IMapper mapper) : IProductService
{
    public async Task<ServiceResponse> AddAsync(CreateProduct product)
    {
        var mappeddata = mapper.Map<Product>(product);
        var result = await productInterface.AddAsync(mappeddata);

        return result > 0 ? new ServiceResponse(true, "product sucess to be added")
            : new ServiceResponse(false, "product failed to be added");
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        var result = await productInterface.DeleteAsync(id);

        return result > 0 ? new ServiceResponse(true,"product deleted!") 
            : new ServiceResponse(false, "prodcut not found or failed to be deleted");
    }

    public async Task<IEnumerable<GetProduct>> GetAllAsync()
    {
        var rawData=await productInterface.GetAllAsync();
        if (!rawData.Any()) return [];
        return mapper.Map<IEnumerable<GetProduct>>(rawData);
    }

    public async Task<GetProduct> GetByIdAsync(Guid id)
    {
        var rawData = await productInterface.GetByIdAsync(id);
        if (rawData==null) return new GetProduct();
        return mapper.Map<GetProduct>(rawData);
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
    {
        var mappeddata = mapper.Map<Product>(product);
        var result = await productInterface.UpdateAsync(mappeddata);

        return result > 0 ? new ServiceResponse(true, "product sucess to be updated")
            : new ServiceResponse(false, "product failed to be updated");
    }
}
