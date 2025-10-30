using FluentValidation;

namespace Application.Features.Todos.Commands.Create
{
    public sealed class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.DueDate).NotEmpty().WithMessage("DueDate is required");
        }
    }
}