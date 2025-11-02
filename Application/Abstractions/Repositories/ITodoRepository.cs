using Application.Contracts.Responses;
using Application.Wrappers;
using Domain.Todos;

namespace Application.Abstractions.Repositories;

public interface ITodoRepository : IGenericRepositoryAsync<Todo>
{
    Task<Pagination<TodoResponse>> GetPaginatedTodosAsync(int pageNumber, int pageSize, string? filterByTitle,
        string? filterByDescription, int? filterByIsCompleted);
}