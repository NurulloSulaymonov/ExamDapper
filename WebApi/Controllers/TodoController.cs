using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController
{
    private TodoService _todoService;
    public TodoController()
    {
        _todoService = new TodoService();
    }
    
    [HttpGet]
    public List<GetTodoDto> Get()
    {
        return _todoService.GetTodos();
    }
    [HttpPost]
    public int Add(AddTodoDto todo)
    {
        return _todoService.AddTodo(todo);
    }
}