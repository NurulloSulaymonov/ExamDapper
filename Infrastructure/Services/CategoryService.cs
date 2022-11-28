using Dapper;
using Domain.Dtos;
using Npgsql;

namespace Infrastructure.Services;

public class CategoryService
{
    public CategoryService()
    {
        
    }
    
    private string _connectionString = "Server=127.0.0.1;Port=5432;Database=dapper_demo;User Id=postgres;Password=12345;";

    public List<CategoryDto> GetCategories()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            
            var categories = connection.Query<CategoryDto>("SELECT c.id, c.Name as CategoryName  FROM categories as c").ToList();
            return categories;
        }
    }
    
    public CategoryDto GetCategoryById(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var category = connection.QueryFirstOrDefault<CategoryDto>($"SELECT * FROM categories WHERE id = {id}");
            return category;
        }
    }
    
    public int AddCategory(CategoryDto category)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var response  = connection.Execute($"INSERT INTO categories (name) VALUES ('{category.CategoryName}')");
            return response;
        }
    }

    public int UpdateCategory(CategoryDto category)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var response  = connection.Execute($"Update categories set name = '{category.CategoryName}' where id = {category.Id};");
            return response;
        } 
    }

    public int DeleteCategory(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var response  = connection.Execute($"delete from categories where id= {id}");
            return response;
        } 
    }
    
    
}