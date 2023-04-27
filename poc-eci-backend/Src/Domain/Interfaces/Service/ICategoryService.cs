using Domain.Entities;

namespace Domain.Interfaces.Service
{
    public interface ICategoryService
    {
        public Task<Category> Create(Category category);
    }
}
