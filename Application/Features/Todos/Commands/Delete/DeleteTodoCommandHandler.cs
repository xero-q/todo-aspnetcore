using MediatR;
using Application.Abstractions.Repositories;

namespace Application.Features.Todos.Commands.Delete
{
    public class DeleteTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (todo is null)
            {
                return false;
            }

            await todoRepository.DeleteByIdAsync(request.Id, cancellationToken);

            return true;
        }
    }
}