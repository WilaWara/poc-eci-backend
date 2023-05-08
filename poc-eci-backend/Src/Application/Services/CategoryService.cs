using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }

        public Task<Category> Create(Category category)
        {
            return _categoryRepository.Create(category);
        }
    }
}
