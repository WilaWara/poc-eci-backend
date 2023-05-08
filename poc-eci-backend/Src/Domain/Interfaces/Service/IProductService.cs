using Domain.Entities;

namespace Domain.Interfaces.Service
{
    public interface IProductService
    {
        public Task<Product> Create(Product product);

        public Task<IEnumerable<Product>> GetAll();

        public Task<IEnumerable<Product>> GetByFilters(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice);

        public Task<Product> Update(int productId, Product product);

        public void Delete(int productId);
    }
}
