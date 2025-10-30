using Application.Contracts.Responses;
using MediatR;

namespace Application.Features.Todos.Commands.Update;

public record UpdateTodoCommand(
    int TodoId,
    string Title,
    string Description,
    DateTime DueDate,
    bool IsCompleted
) : IRequest<TodoResponse>;