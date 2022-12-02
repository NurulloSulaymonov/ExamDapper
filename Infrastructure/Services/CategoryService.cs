using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class CategoryService
{
    private DapperContext _context;
    private readonly IWebHostEnvironment _env;

    public CategoryService(DapperContext context,IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }
    

    public async Task<Response<List<CategoryDto>>> GetCategories()
    {
        using (var connection = _context.CreateConnection())
        {
            var categories = await connection.QueryAsync<CategoryDto>("SELECT c.id, c.Name as CategoryName  FROM categories as c");
            return new Response<List<CategoryDto>>(categories.ToList());
        }
    }
    
    public CategoryDto GetCategoryById(int id)
    {
        using (var connection = _context.CreateConnection())

        {
            var category = connection.QueryFirstOrDefault<CategoryDto>($"SELECT * FROM categories WHERE id = {id}");
            return category;
        }
    }
    
    public async Task<Response<CategoryDto>> AddCategory(CategoryDto category)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var response  = await connection.ExecuteScalarAsync<int>($"INSERT INTO categories (name) VALUES ('{category.CategoryName}') returning id");
                category.Id = response;
                return new Response<CategoryDto>(category);
            }
        }
        catch (Exception ex)
        {
            return new Response<CategoryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
      
    }

    public int UpdateCategory(CategoryDto category)
    {
        using (var connection = _context.CreateConnection())

        {
            var response  = connection.Execute($"Update categories set name = '{category.CategoryName}' where id = {category.Id};");
            return response;
        } 
    }

    public async Task<Response<string>> DeleteCategory(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var response = await connection.ExecuteAsync($"delete from categories where id= {id}");
            if(response > 0)
                return new Response<string>("Category deleted successfully");
        
            return new Response<string>(HttpStatusCode.BadRequest, "Category not found");
        } 
    }
    
    
}