using EcommerceApp.Application.DTOs.Product;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Host.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var data = await productService.GetAllAsync();

        return data.Any() ? Ok(data) : NotFound(data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await productService.GetByIdAsync(id);

        return data != null ? Ok(data) : NotFound(data);
    }
    [HttpPost("")]
    public async Task<IActionResult>Add(CreateProduct product)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);

       var result= await productService.AddAsync(product);
        return result.success ? Ok(result) : BadRequest(result);
    }
    [HttpPut("")]
    public async Task<IActionResult>Update(UpdateProduct product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await productService.UpdateAsync(product);
        return result.success ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult>Delete(Guid id)
    {
        var result = await productService.DeleteAsync(id);
        return result.success ? Ok(result) : BadRequest(result);
    }
}
