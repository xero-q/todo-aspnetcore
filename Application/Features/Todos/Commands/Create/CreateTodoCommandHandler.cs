using MediatR;
using Application.Abstractions.Repositories;
using Application.Contracts.Responses;
using Application.Mappings;
using FluentValidation;

namespace Application.Features.Todos.Commands.Create
{
    public class CreateTodoCommandHandler(IValidator<CreateTodoCommand> validator, ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, TodoResponse>
    {
        public async Task<TodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAsync(request, cancellationToken);
           
            var todo = request.MapToTodo();

            await todoRepository.CreateAsync(todo, cancellationToken);
           
            return todo.MapToResponse();
        }
    }
}