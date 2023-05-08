using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        public Task<Product> Create(Product product);

        public Task<IEnumerable<Product>> GetAll();

        public Product GetById(int productId);

        public Task<IEnumerable<Product>> GetByFilters(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice);

        public Task<Product> Update(int productId, Product product);

        public void Delete(int productId);
    }
}
