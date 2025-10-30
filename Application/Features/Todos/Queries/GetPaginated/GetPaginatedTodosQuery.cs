using Application.Contracts.Responses;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Todos.Queries.GetPaginated
{
    public record GetPaginatedTodosQuery(
        int PageNumber,
        int PageSize,
        string? FilterByTitle,
        string? FilterByDescription,
        int? FilterByIsCompleted
) : IRequest<Pagination<TodoResponse>>;
}