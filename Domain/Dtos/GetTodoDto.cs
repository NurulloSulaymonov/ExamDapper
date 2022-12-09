using Domain.Entities;

namespace Domain.Dtos;

public class GetTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public string TodoListTitle { get; set; }
    public string  TodoListColor { get; set; }
}