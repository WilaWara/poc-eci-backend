using Application.CQRS.Commands.User;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Domain.Entities.User>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Domain.Entities.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new Domain.Entities.User
            {
                Name = request.name,
                Email = request.email,
                Password = request.password,
                Role = request.role
            };
            return await _userRepository.Create(newUser);
        }
    }
}
