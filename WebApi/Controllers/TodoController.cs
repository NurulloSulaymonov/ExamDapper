using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }
    
    [HttpGet]
    public async Task<List<GetTodoDto>> Get()
    {
        return  await _todoService.GetTodos();
    }
    
    [HttpPost]
    public async Task<AddTodoDto> Post(AddTodoDto todo)
    {
        return await _todoService.Add(todo);
    }
    
    [HttpPut]
    public async Task<Todo> Put(Todo todo)
    {
        return await _todoService.Update(todo);
    }
    
    [HttpDelete]
    public async Task Delete(int id)
    {
        await _todoService.Delete(id);
    }
}