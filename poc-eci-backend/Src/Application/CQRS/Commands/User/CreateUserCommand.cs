using MediatR;

namespace Application.CQRS.Commands.User
{
    public record CreateUserCommand(
        string name, 
        string email, 
        string password, 
        string role
    ) : IRequest<Domain.Entities.User>;
}
