using Domain.Entities;

namespace Domain.Dtos;

public class AddTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public int TodoListId { get; set; }
}