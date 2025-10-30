using MediatR;

namespace Application.Features.Todos.Commands.Delete;

public record DeleteTodoCommand(
    int Id
) : IRequest<bool>;