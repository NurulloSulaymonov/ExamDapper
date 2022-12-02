namespace Domain.Dtos;

public class GetTodoDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string StatusName
    {
        get { return Status.ToString();}  
    }
    public Status Status { get; set; }
    
}