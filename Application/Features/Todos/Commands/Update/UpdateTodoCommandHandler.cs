using MediatR;
using Application.Abstractions.Repositories;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Mappings;
using ValidationException = Application.Exceptions.ValidationException;
using FluentValidation;

namespace Application.Features.Todos.Commands.Update
{
    public class UpdateTodoCommandHandler(IValidator<UpdateTodoCommand> validator, ITodoRepository todoRepository) : IRequestHandler<UpdateTodoCommand, TodoResponse>
    {
        public async Task<TodoResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var todo = await todoRepository.GetByIdAsync(request.TodoId, cancellationToken);

            if (todo is null)
            {
                throw new NotFoundException("Todo", request.TodoId);
            }
            
            var existingTodo = await todoRepository.GetByTitleAsync(request.Title, cancellationToken);

            if (existingTodo is not null && existingTodo.TodoId != request.TodoId)
            {
                throw new ValidationException("There is already a TODO with this Title");   
            }

            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.DueDate = request.DueDate;
            todo.IsCompleted = request.IsCompleted;

            await todoRepository.UpdateAsync(todo, request.TodoId, cancellationToken);

            return todo.MapToResponse();
        }
    }
}