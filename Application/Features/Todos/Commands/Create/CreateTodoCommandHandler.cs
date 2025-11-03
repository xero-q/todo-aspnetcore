using MediatR;
using Application.Abstractions.Repositories;
using Application.Contracts.Responses;
using Application.Mappings;
using ValidationException = Application.Exceptions.ValidationException;
using FluentValidation;

namespace Application.Features.Todos.Commands.Create
{
    public class CreateTodoCommandHandler(IValidator<CreateTodoCommand> validator, ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, TodoResponse>
    {
        public async Task<TodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            
            var existingTodo = await todoRepository.GetByTitleAsync(request.Title, cancellationToken);

            if (existingTodo is not null)
            {
                throw new ValidationException("There is already a TODO with this Title");
            }

            var todo = request.MapToTodo();

            await todoRepository.CreateAsync(todo, cancellationToken);

            return todo.MapToResponse();
        }
    }
}