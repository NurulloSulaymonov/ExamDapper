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
    
    public async Task<List<Todo>> GetTodos()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo> Add(Todo todo)
    {
        _context.Todos.Add(todo);
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