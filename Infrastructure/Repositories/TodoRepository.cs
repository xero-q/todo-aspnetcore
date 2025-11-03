using Application.Abstractions.Repositories;
using Application.Contracts.Responses;
using Application.Mappings;
using Application.Wrappers;
using Domain.Todos;
using Infrastructure.Database.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TodoRepository(ApplicationDbContext context) : GenericRepositoryAsync<Todo>(context), ITodoRepository
{
    public async Task<Pagination<TodoResponse>> GetPaginatedTodosAsync(int pageNumber, int pageSize, string? filterByTitle, string? filterByDescription, int? filterByIsCompleted)
    {
        // Base query
        var query = context.Todos.AsQueryable();

        // Apply filter by Title if provided
        if (!string.IsNullOrWhiteSpace(filterByTitle))
        {
            string search = $"%{filterByTitle.Trim()}%";
            query = query.Where(t =>
                EF.Functions.Like(t.Title, search));
        }

        // Apply filter by Description if provided
        if (!string.IsNullOrWhiteSpace(filterByDescription))
        {
            string search = $"%{filterByDescription.Trim()}%";
            query = query.Where(t =>
                EF.Functions.Like(t.Description, search));
        }

        // Apply filter by Status if provided
        if (filterByIsCompleted is not null)
        {
            bool isCompleted = filterByIsCompleted == 1;
            query = query.Where(t => t.IsCompleted == isCompleted);
        }

        // Calculate total count without materializing the query
        var totalCountQuery = await query.CountAsync();

        // Ensure valid pageNumber
        pageNumber = Math.Max(1, Math.Min(pageNumber, (int)Math.Ceiling((double)totalCountQuery / pageSize)));

        // Calculate skip
        var skip = (pageNumber - 1) * pageSize;

        var results = await query
            .OrderBy(t => t.TodoId)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var mappedResults = results.Select(t => t.MapToResponse()
     ).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCountQuery / pageSize);

        return new Pagination<TodoResponse>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalCountQuery,
            Result = mappedResults.ToList()
        };
    }

    public async Task<Todo?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await context.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => EF.Functions.Like(t.Title.Trim(), title.Trim()), cancellationToken);
    }
}