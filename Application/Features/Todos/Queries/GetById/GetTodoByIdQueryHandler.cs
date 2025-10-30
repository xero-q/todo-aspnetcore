using MediatR;
using Application.Abstractions.Repositories;
using Application.Contracts.Responses;
using Application.Exceptions;
using Application.Mappings;

namespace Application.Features.Todos.Queries.GetById
{
    public class GetTodoByIdQueryHandler(ITodoRepository todoRepository) : IRequestHandler<GetTodoByIdQuery, TodoResponse>
    {
        public async Task<TodoResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (todo is null)
            {
                throw new NotFoundException("Todo",request.Id);
            }

            return todo.MapToResponse();
        }
    }
}