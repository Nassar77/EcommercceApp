using EcommerceApp.Application.DTOs.Category;
using EcommerceApp.Application.DTOs.Product;
using EcommerceApp.Application.Services.Implemantions;
using EcommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CategorysController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var data = await categoryService.GetAllAsync();

        return data.Any() ? Ok(data) : NotFound(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await categoryService.GetByIdAsync(id);

        return data != null ? Ok(data) : NotFound(data);
    }
    [HttpPost("")]
    public async Task<IActionResult> Add(CreateCategory category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await categoryService.AddAsync(category);
        return result.success ? Ok(result) : BadRequest(result);
    }
    [HttpPut("")]
    public async Task<IActionResult> Update(UpdateCategory category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await categoryService.UpdateAsync(category);
        return result.success ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await categoryService.DeleteAsync(id);
        return result.success ? Ok(result) : BadRequest(result);
    }
}
