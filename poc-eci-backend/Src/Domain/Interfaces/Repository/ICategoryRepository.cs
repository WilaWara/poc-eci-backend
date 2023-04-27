using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        public Task<Category> Create(Category category);
    }
}
