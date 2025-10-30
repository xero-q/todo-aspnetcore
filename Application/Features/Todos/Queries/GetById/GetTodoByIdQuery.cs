using Application.Contracts.Responses;
using MediatR;

namespace Application.Features.Todos.Queries.GetById
{
    public record GetTodoByIdQuery(
        int Id
) : IRequest<TodoResponse>;
}