using FluentValidation;

namespace Application.Features.Todos.Commands.Update
{
    public sealed class UpdateTodoValidator : AbstractValidator<UpdateTodoCommand>
    {
        public UpdateTodoValidator()
        {
            RuleFor(x => x.TodoId).NotEmpty().WithMessage("TodoId is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x=>x.DueDate).NotEmpty().WithMessage("DueDate is required");
            RuleFor(x => x.IsCompleted).NotNull().WithMessage("IsCompleted is required");
        }
    }
}