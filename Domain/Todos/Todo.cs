namespace Domain.Todos;

public class Todo
{
    public int TodoId { get; init; }

    public string Title { get; set; }
    
    public string Description { get; set; }
    
    private DateTime _dueDate;
    public DateTime DueDate
    {
        get => _dueDate;
        set => _dueDate = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
    }
    
    public bool IsCompleted { get; set; }
}