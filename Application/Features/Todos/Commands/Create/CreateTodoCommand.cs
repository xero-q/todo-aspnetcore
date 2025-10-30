using Application.Contracts.Responses;
using MediatR;

namespace Application.Features.Todos.Commands.Create;

public record CreateTodoCommand(
    string Title,
    string Description,
    DateTime DueDate
) : IRequest<TodoResponse>;