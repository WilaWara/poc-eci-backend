using Domain.Entities;

namespace Domain.Interfaces.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        public Task<Category> Create(Category category);
    }
}
