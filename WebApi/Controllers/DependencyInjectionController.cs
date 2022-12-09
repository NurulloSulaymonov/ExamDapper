using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DependencyInjectionController
{
    private readonly SingletonsService _singletonsService;
    private readonly ScopedService _scopedService;
    private readonly TodoListService _todoService;
    private readonly TransientService _transientService;

    public DependencyInjectionController(SingletonsService singletonsService,
        ScopedService scopedService,
        TodoListService todoService,
        TransientService transientService)
    {
        _singletonsService = singletonsService;
        _scopedService = scopedService;
        _todoService = todoService;
        _transientService = transientService;
    }

    [HttpGet("GetSingletonsService")]
    public string GetSingleton()
    {
        return _singletonsService.Id;
    }
    
    [HttpGet("GetScopedService")]
    public List<string> GetScopedService()
    {
        
        var id1 =  _scopedService.Id;
        var id2 = _todoService.GetId();
        
        return new List<string> {id1, id2};

    }
    [HttpGet("GetTransientService")]
    public List<string> GetTransientService()
    {
        
        var id1 =  _transientService.Id;
        var id2 = _todoService.GetTransientId();
        
        return new List<string> {id1, id2};

    }
}