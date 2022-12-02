using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController
{
    private CategoryService _categoryService;
    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    

    [HttpGet("GetCategory")]
    public async Task<Response<List<CategoryDto>>> GetCategories()=> await _categoryService.GetCategories();

    [HttpPost("AddCategory")]
    public async Task<Response<CategoryDto>> AddCategory(CategoryDto categoryDto)
    {
        return await _categoryService.AddCategory(categoryDto);
    } 
    
}