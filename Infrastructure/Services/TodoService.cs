using Dapper;
using Domain.Dtos;
using Npgsql;

namespace Infrastructure.Services;

public class TodoService
{
    
    private string _connectionString = "Server=127.0.0.1;Port=5432;Database=dapper_demo;User Id=postgres;Password=12345;";

    public List<GetTodoDto> GetTodos()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            
            var todos = connection.Query<GetTodoDto>("SELECT *  FROM Todos").ToList();
            return todos;
        }
    }
    
    public int AddTodo(AddTodoDto todo)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var affectedRows = connection.Execute($"INSERT INTO Todos (Title, Status) VALUES ('{todo.Title}',{(int)todo.Status})");
            return affectedRows;
        }
    }
}