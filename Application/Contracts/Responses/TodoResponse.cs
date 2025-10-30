namespace Application.Contracts.Responses;

public sealed class TodoResponse
{
    public int TodoId { get; init; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public bool IsCompleted { get; set; }
}