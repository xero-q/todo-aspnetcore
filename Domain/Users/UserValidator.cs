using FluentValidation;

namespace Domain.Users;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Username).NotEmpty().MinimumLength(3);
        RuleFor(u => u.Password).NotEmpty().MinimumLength(3);
    }
}