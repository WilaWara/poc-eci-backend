using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _db;

        public UserRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<User> Create(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            User? user = _db.Users
                .FirstOrDefault(
                    u => u.Email.ToLower() == email.ToLower() &&
                    u.Password == password
                );
            return user;
        }

    }
}
