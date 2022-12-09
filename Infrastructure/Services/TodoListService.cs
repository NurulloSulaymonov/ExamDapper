using Domain.Dtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TodoListService
{
    private readonly DataContext _context;
    private readonly ScopedService _scopedService;
    private readonly TransientService _transientService;

    public TodoListService(DataContext context, 
        ScopedService scopedService,
        TransientService transientService)
    {
        _context = context;
        _scopedService = scopedService;
        _transientService = transientService;
    }

    public string GetId()
    {
        return _scopedService.Id;
    }
    public string GetTransientId()
    {
        return _transientService.Id;
    }
    public async Task<List<GetTodoListDto>> Todos()
    {
        var list =await _context.TodoLists.Select(t => new GetTodoListDto()
        {
         Id = t.Id,
         Title = t.Title,
         Color = t.Color
        }).ToListAsync();

        return list;
    }

    public async Task<AddTodoListDto> Update(AddTodoListDto todo)
    {
        var find = await _context.TodoLists.FindAsync(todo.Id);
        if (find == null) return new AddTodoListDto();
        find.Color = todo.Color;
        find.Title = todo.Title;

        await _context.SaveChangesAsync();
        return todo;

    }
}