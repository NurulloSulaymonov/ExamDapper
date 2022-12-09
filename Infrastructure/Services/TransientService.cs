namespace Infrastructure.Services;

public class TransientService
{
    public string Id { get; set; }
    
    public TransientService()
    {
        Id = Guid.NewGuid().ToString();
    }
}