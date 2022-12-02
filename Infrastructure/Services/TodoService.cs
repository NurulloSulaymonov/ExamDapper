using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class TodoService
{
    private readonly DapperContext _context;
    private readonly IWebHostEnvironment _hosting;

    public TodoService(DapperContext context,IWebHostEnvironment hosting)
    {
        _context = context;
        _hosting = hosting;
    }
     

    public List<GetTodoDto> GetTodos()
    {
        using (var connection = _context.CreateConnection())
        {
            
            var todos = connection.Query<GetTodoDto>("SELECT *  FROM Todos").ToList();
            return todos;
        }
    }
    
    public async Task<Response<GetTodoDto>> AddTodo(AddTodoDto todo)
    {
        using (var connection = _context.CreateConnection())
        {
            var path = Path.Combine(_hosting.WebRootPath, "todoimages",todo.File.FileName);
            
            using (var stream = File.Create(path))
            {
                await todo.File.CopyToAsync(stream);
            }
            var insertedId = await connection.ExecuteScalarAsync<int>($"INSERT INTO Todos (Title, Status,filename) VALUES ('{todo.Title}',{(int)todo.Status},'{todo.File.FileName}') returning id");
            todo.Id = insertedId;
            var response = new GetTodoDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                FileName = todo.File.FileName,
                Status = todo.Status
            };
            return new Response<GetTodoDto>(response);
        }
    }
}