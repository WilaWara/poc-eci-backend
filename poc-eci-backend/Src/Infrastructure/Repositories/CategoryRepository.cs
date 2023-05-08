using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _db;

        public CategoryRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return _db.Categories
                .OrderBy(c => c.Id)
                .ToList();
        }

        public async Task<Category> Create(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }
    }
}
