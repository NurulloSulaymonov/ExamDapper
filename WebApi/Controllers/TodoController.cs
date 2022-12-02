using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController
{
    private TodoService _todoService;
    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }
    
    
    [HttpGet]
    public List<GetTodoDto> Get()
    {
        return _todoService.GetTodos();
    }
    [HttpPost]
    public async Task<Response<GetTodoDto>> Add([FromForm] AddTodoDto todo)
    {
        return await _todoService.AddTodo(todo);
    }
}