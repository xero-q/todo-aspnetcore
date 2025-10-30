using MediatR;

namespace Application.Features.Users.Commands.Create;

public record CreateUserCommand(
    string Username,
    string Password
    ) : IRequest<bool>;