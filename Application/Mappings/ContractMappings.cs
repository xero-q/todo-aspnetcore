using Application.Contracts.Responses;
using Application.Features.Todos.Commands.Create;
using Domain.Todos;

namespace Application.Mappings;

public static class ContractMappings
{
   public static Todo MapToTodo(this CreateTodoCommand request)
    {
        return new Todo
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            IsCompleted = false
        };
    }
    public static TodoResponse MapToResponse(this Todo todo)
    {
        return new TodoResponse
        {
            TodoId = todo.TodoId,
            Title = todo.Title,
            Description = todo.Description,
            DueDate = DateTime.SpecifyKind(todo.DueDate, DateTimeKind.Utc),
            IsCompleted = todo.IsCompleted
            
        };
    }
}