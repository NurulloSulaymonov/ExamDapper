using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TodoService
{
    private readonly DataContext _context;
    public TodoService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<GetTodoDto>> GetTodos()
    {
        var list  = await _context.Todos.Select(t => new GetTodoDto()
        {
            Id = t.Id,
            Description = t.Description,
            Status = t.Status,
            Title = t.Title,
            CreatedAt = t.CreatedAt,
            TodoListTitle = t.TodoList.Title,
            TodoListColor = t.TodoList.Color
        }).ToListAsync();

        return list;
    }

    public async Task<AddTodoDto> Add(AddTodoDto todo)
    {
        var newTodo = new Todo()
        {
            Description = todo.Description,
            Status = todo.Status,
            Title = todo.Title,
            CreatedAt = DateTime.UtcNow,
            TodoListId = todo.TodoListId
        };
        _context.Todos.Add(newTodo);
        await _context.SaveChangesAsync();
        return todo;
    }
    
    public async Task<Todo> Update(Todo todo)
    {
        
        var find = await _context.Todos.FindAsync(todo.Id);
        find.Title = todo.Title;
        find.Description = todo.Description;
        find.Status = todo.Status;
        find.CreatedAt = todo.CreatedAt;
        await _context.SaveChangesAsync();
        return todo;
    }
    
    public async Task<string> Delete(int id)
    {
        var find = await _context.Todos.FindAsync(id);
        await _context.SaveChangesAsync();
        return "Deleted";
    }
}