namespace Domain.Dtos;

public class AddTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Status Status { get; set; }
}

public class GetTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string StatusName
    {
        get { return Status.ToString();}  
    }
    public Status Status { get; set; }
    
}

public enum Status
{
    Todo,
    InProgress,
    Completed
}