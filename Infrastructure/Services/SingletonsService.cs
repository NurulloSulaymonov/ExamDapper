namespace Infrastructure.Services;

public class SingletonsService
{
    public string Id { get; set; }
    
    public SingletonsService()
    {
        Id = Guid.NewGuid().ToString();
    }
}