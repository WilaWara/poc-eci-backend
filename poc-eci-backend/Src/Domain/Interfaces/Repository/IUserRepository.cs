using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        public Task<User> Create(User user);

        Task<User> Login(string email, string password);
    }
}
