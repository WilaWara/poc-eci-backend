using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public Task<IEnumerable<Product>> GetByFilters(string? name, string? category, decimal? minPrice, decimal? maxPrice)
        {
            return _productRepository.GetByFilters(name, category, minPrice, maxPrice);
        }

        public Task<Product> Create(Product product)
        {
            return _productRepository.Create(product);
        }

        public Task<Product> Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public void Delete(int productId)
        {
            _productRepository.Delete(productId);
        }
    }
}
