using EcommerceApp.Application.DTOs.Category;
using EcommerceApp.Application.DTOs.Common;

namespace EcommerceApp.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<GetCategory>> GetAllAsync();
    Task<GetCategory> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateCategory category);
    Task<ServiceResponse> UpdateAsync(UpdateCategory category);
    Task<ServiceResponse> DeleteAsync(Guid id);
}

