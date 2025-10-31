using Moq;
using Application.Features.Todos.Commands.Create;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class CreateTodoCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_CreateTodoAndReturnResponse()
    {
        // Arrange
        var dbContext = TestDbContextFactory.CreateInMemoryDbContext();
        var todoRepository = new TodoRepository(dbContext); // real implementation

        var validatorMock = new Mock<IValidator<CreateTodoCommand>>();
        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<CreateTodoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var handler = new CreateTodoCommandHandler(validatorMock.Object, todoRepository);

        var command = new CreateTodoCommand(
            Title: "Test Todo",
            Description: "Test description",
            DueDate: DateTime.UtcNow
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Title, result.Title);
        Assert.Equal(command.Description, result.Description);

        // Check it is actually saved in the database
        var savedTodo = await dbContext.Todos.FirstOrDefaultAsync(t => t.Title == command.Title);
        Assert.NotNull(savedTodo);
        Assert.Equal(command.Description, savedTodo.Description);
    }
}