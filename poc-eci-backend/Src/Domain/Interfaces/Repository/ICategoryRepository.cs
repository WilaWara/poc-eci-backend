using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        public Task<Category> Create(Category category);
    }
}
