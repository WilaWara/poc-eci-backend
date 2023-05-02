using Domain.Entities;

namespace Domain.Interfaces.Service
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        string EncodeMD5(string password);
        Task<IDictionary<User, string>> Login(string email, string password);
        IDictionary<User, string> GetToken(User user);

    }
}
