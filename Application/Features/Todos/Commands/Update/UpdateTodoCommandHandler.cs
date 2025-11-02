using MediatR;
using Application.Abstractions.Repositories;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Mappings;
using FluentValidation;

namespace Application.Features.Todos.Commands.Update
{
    public class UpdateTodoCommandHandler(IValidator<UpdateTodoCommand> validator, ITodoRepository todoRepository) : IRequestHandler<UpdateTodoCommand, TodoResponse>
    {
        public async Task<TodoResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAsync(request, cancellationToken);

            var todo = await todoRepository.GetByIdAsync(request.TodoId, cancellationToken);

            if (todo is null)
            {
                throw new NotFoundException("Todo", request.TodoId);
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