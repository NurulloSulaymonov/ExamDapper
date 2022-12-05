using Dapper;
using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class TodoService
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly DapperContext _context;

    public TodoService(IWebHostEnvironment hostEnvironment, DapperContext context)
    {
        _hostEnvironment = hostEnvironment;
        _context = context;
    }

    public async Task<GetTodoDto> AddToto(AddTodoDto todo)
    {
        var response = new GetTodoDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            File = todo.File.FileName
        };
        
        var path = Path.Combine(_hostEnvironment.WebRootPath, "images");
        //check for directory
        if (Directory.Exists(path) == false)
            Directory.CreateDirectory(path);
        
        var filepath =Path.Combine(path,todo.File.FileName);
        using (var file = File.Create(filepath))
        {
            await todo.File.CopyToAsync(file);
        }

        using (var connection = _context.Connection())
        {
            var sql = $"INSERT INTO Todos ( Title,status, filename) VALUES ( '{todo.Title}', {(int)todo.Status}, '{todo.File.FileName}') returning id";
            var insertedId = await connection.ExecuteScalarAsync<int>(sql);
            response.Id = insertedId;
        }

        return response;
    }

    public async Task<GetTodoDto> UpdateTodo(AddTodoDto todo)
    {
        var response = new GetTodoDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            File = todo.File.FileName
        };
      
      // for deleting file 
      if (todo.File != null)
      {
          //removing old file
          using (var connection = _context.Connection())
          {
             var existingTodo =
                  connection.QuerySingleOrDefault<GetTodoDto>(
                      $"Select id, title, status, filename as File from todos where id = {todo.Id}");

              var existingFile = Path.Combine(_hostEnvironment.WebRootPath, "images", existingTodo.File);
              if (File.Exists(existingFile))
              {
                  File.Delete(existingFile);
              }
          } 
      }
       
        //update if file is not found 
        if (todo.File == null)
        {
            using (var connection = _context.Connection())
            {
                var sql = $"update todos set title = '{todo.Title}', status = {(int)todo.Status} where id = {todo.Id}";
                connection.ExecuteAsync(sql);
            }
        }
        else
        {
            var newfile = Path.Combine(_hostEnvironment.WebRootPath, "images", todo.File.FileName);
            using (var file = File.Create(newfile))
            {
                await todo.File.CopyToAsync(file);
            }
            using (var connection = _context.Connection())
            {
                var sql = $"update todos set title = '{todo.Title}', status = {(int)todo.Status}, filename = {todo.File.FileName} where id = {todo.Id}";
                connection.ExecuteAsync(sql);
            }
        }

        return response;

    }
}