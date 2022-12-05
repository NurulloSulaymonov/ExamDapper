using Domain.Dtos;
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

    [HttpPost("AddTodo")]
    public async Task<GetTodoDto> AddTodo([FromForm]AddTodoDto todoDto)
    {
        return await _todoService.AddToto(todoDto);
    } 
    
    [HttpPut("UpdateTodo")]
    public async Task<GetTodoDto> UpdateTodo([FromForm]AddTodoDto todoDto)
    {
        return await _todoService.UpdateTodo(todoDto);
    } 
    
}