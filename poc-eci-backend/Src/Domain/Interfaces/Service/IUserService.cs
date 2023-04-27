using Domain.Entities;

namespace Domain.Interfaces.Service
{
    public interface IUserService
    {
        public Task<User> Create(User user);
    }
}
