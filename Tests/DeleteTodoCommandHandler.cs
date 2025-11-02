using Application.Features.Todos.Commands.Delete;
using Domain.Todos;
using Infrastructure.Repositories;

namespace Tests;

public class DeleteTodoCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_DeleteTodo_WhenTodoExists()
    {
        // Arrange
        var dbContext = TestDbContextFactory.CreateInMemoryDbContext();
        var todoRepository = new TodoRepository(dbContext);

        // Create a todo to delete
        var todo = new Todo
        {
            Title = "Test Delete",
            Description = "Delete me",
            DueDate = DateTime.UtcNow,
            IsCompleted = false
        };
        await todoRepository.CreateAsync(todo, CancellationToken.None);

        var handler = new DeleteTodoCommandHandler(todoRepository);
        var command = new DeleteTodoCommand(Id: todo.TodoId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);

        var deletedTodo = await dbContext.Todos.FindAsync(todo.TodoId);
        Assert.Null(deletedTodo);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse_WhenTodoDoesNotExist()
    {
        // Arrange
        var dbContext = TestDbContextFactory.CreateInMemoryDbContext();
        var todoRepository = new TodoRepository(dbContext);

        var handler = new DeleteTodoCommandHandler(todoRepository);
        var command = new DeleteTodoCommand(Id: 999); // non-existing Id

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}