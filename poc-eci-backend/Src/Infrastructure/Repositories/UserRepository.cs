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
    }
}
