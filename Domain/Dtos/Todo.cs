using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class AddTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IFormFile File { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    Todo,
    InProgress,
    Completed
}