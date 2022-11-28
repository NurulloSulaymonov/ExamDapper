using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController
{
    private CategoryService _categoryService;
    public CategoryController()
    {
        _categoryService = new CategoryService();
    }


    [HttpGet("GetCategory")]
    public List<CategoryDto> GetCategories()=> _categoryService.GetCategories();

    [HttpPost("AddCategory")]
    public int AddCategory(CategoryDto categoryDto)
    {
        return _categoryService.AddCategory(categoryDto);
    } 
    
}