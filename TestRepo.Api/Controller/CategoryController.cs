using Microsoft.AspNetCore.Mvc;
using TestRepo.Service.Category;

namespace TestRepo.Api.Controller;

[ApiController]  
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IService _categoryService;

    public CategoryController(IService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateCategory(Request.CategoryRequest request)
    {
        var  newCate = await _categoryService.CreateCategory(request);
        return Ok(newCate);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategories();
        return Ok(categories);
    }
    
    [HttpGet("{parentId}/children")]
    public async Task<IActionResult> GetAllCategories(Guid parentId)
    {
        var categories = await _categoryService.GetAllCategories(parentId);
        return Ok(categories);
    }
}