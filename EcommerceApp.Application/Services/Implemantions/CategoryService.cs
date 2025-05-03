using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.DTOs.Category;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces;

namespace EcommerceApp.Application.Services.Implemantions;
public class CategoryService(IGeneric<Category> categoryInterface,IMapper mapper) : ICategoryService
{
    public async Task<ServiceResponse> AddAsync(CreateCategory category)
    {
        var mappeddata = mapper.Map<Category>(category);
        var result = await categoryInterface.AddAsync(mappeddata);

        return result > 0 ? new ServiceResponse(true, "Category sucess to be added")
            : new ServiceResponse(false, "Category failed to be added");
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        var result = await categoryInterface.DeleteAsync(id);
    
        return result > 0 ? new ServiceResponse(true, "Category deleted!")
            : new ServiceResponse(false, "Category not found or failed to be deleted");
    }

    public async Task<IEnumerable<GetCategory>> GetAllAsync()
    {
        var rawData = await categoryInterface.GetAllAsync();
        if (!rawData.Any()) return [];
        return mapper.Map<IEnumerable<GetCategory>>(rawData);
    }

    public async Task<GetCategory> GetByIdAsync(Guid id)
    {
        var rawData = await categoryInterface.GetByIdAsync(id);
        if (rawData == null) return new GetCategory();
        return mapper.Map<GetCategory>(rawData);
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
    {
        var mappeddata = mapper.Map<Category>(category);
        var result = await categoryInterface.UpdateAsync(mappeddata);

        return result > 0 ? new ServiceResponse(true, "category sucess to be updated")
            : new ServiceResponse(false, "category failed to be updated");
    }
}
