namespace Application.Contracts.Requests;

public sealed class UpdateTodoRequest
{
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required DateTime DueDate { get; init; }

    public required bool IsCompleted { get; init; }
}