using Application.Features.Todos.Queries.GetPaginated;
using Domain.Todos;
using Infrastructure.Repositories;

namespace Tests;
public class GetPaginatedTodosQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnPaginatedTodos()
    {
        // Arrange
        var dbContext = TestDbContextFactory.CreateInMemoryDbContext();
        var todoRepository = new TodoRepository(dbContext);

        // Seed some todos
        for (int i = 1; i <= 10; i++)
        {
            await todoRepository.CreateAsync(new Todo
            {
                Title = $"Todo {i}",
                Description = $"Description {i}",
                DueDate = DateTime.UtcNow.AddDays(i),
                IsCompleted = i % 2 == 0
            }, CancellationToken.None);
        }

        var handler = new GetPaginatedTodosQueryHandler(todoRepository);

        // Act - first page, 5 items per page
        var query = new GetPaginatedTodosQuery(
            PageNumber: 1,
            PageSize: 5,
            FilterByTitle: null,
            FilterByDescription: null,
            FilterByIsCompleted: null
        );

        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Result.Count());
        Assert.Equal(10, result.TotalItems);
        Assert.Equal(2, result.TotalPages);
    }

    [Fact]
    public async Task Handle_Should_ApplyTitleFilter()
    {
        // Arrange
        var dbContext = TestDbContextFactory.CreateInMemoryDbContext();
        var todoRepository = new TodoRepository(dbContext);

        await todoRepository.CreateAsync(new Todo
        {
            Title = "Special Todo",
            Description = "Test",
            DueDate = DateTime.UtcNow,
            IsCompleted = false
        }, CancellationToken.None);

        await todoRepository.CreateAsync(new Todo
        {
            Title = "Another Todo",
            Description = "Test",
            DueDate = DateTime.UtcNow,
            IsCompleted = false
        }, CancellationToken.None);

        var handler = new GetPaginatedTodosQueryHandler(todoRepository);

        var query = new GetPaginatedTodosQuery
        (
            PageNumber: 1,
            PageSize: 10,
            FilterByTitle: "Special",
            FilterByDescription: null,
            FilterByIsCompleted: null
        );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result.Result);
        Assert.Equal("Special Todo", result.Result.First().Title);
    }
}
