using Application.Abstractions.Repositories;
using Application.Features.Todos.Commands.Update;
using Domain.Todos;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Tests;

public class UpdateTodoCommandHandlerTests
{
    private readonly Mock<IValidator<UpdateTodoCommand>> _validatorMock;
    private readonly Mock<ITodoRepository> _todoRepositoryMock;
    private readonly UpdateTodoCommandHandler _handler;

    public UpdateTodoCommandHandlerTests()
    {
        _validatorMock = new Mock<IValidator<UpdateTodoCommand>>();
        _todoRepositoryMock = new Mock<ITodoRepository>();

        _handler = new UpdateTodoCommandHandler(
            _validatorMock.Object,
            _todoRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldUpdateTodo_WhenTodoExistsAndValidationSucceeds()
    {
        // Arrange
        var todoId = 1000;
        var existingTodo = new Todo
        {
            TodoId = todoId,
            Title = "Old title",
            Description = "Old description",
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        var command = new UpdateTodoCommand(
            TodoId: todoId,
            Title: "Updated title",
            Description: "Updated description",
            DueDate: DateTime.UtcNow.AddDays(2),
            IsCompleted: true
        );

        _validatorMock
            .Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _todoRepositoryMock
            .Setup(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingTodo);

        _todoRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Todo>(), todoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Title, result.Title);
        Assert.Equal(command.Description, result.Description);
        Assert.True(result.IsCompleted);

        _todoRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Todo>(t =>
            t.Title == command.Title &&
            t.Description == command.Description &&
            t.DueDate == command.DueDate &&
            t.IsCompleted == command.IsCompleted
        ), todoId, It.IsAny<CancellationToken>()), Times.Once);
    }
}