using Application.Wrappers;
using MediatR;
using Application.Abstractions.Repositories;
using Application.Contracts.Responses;

namespace Application.Features.Todos.Queries.GetPaginated
{
    public class GetPaginatedTodosQueryHandler(ITodoRepository todoRepository)
        : IRequestHandler<GetPaginatedTodosQuery, Pagination<TodoResponse>>
    {
        public async Task<Pagination<TodoResponse>> Handle(GetPaginatedTodosQuery request,
            CancellationToken cancellationToken)
        {
            return await todoRepository.GetPaginatedTodosAsync(
                request.PageNumber, request.PageSize, request.FilterByTitle, request.FilterByDescription,
                request.FilterByIsCompleted);
        }
    }
}