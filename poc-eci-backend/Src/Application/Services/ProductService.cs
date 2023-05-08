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

        public Task<IEnumerable<Product>> GetByFilters(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            return _productRepository.GetByFilters(name, categoryId, minPrice, maxPrice);
        }

        public Task<Product> Create(Product product)
        {
            product.Enabled = true;
            return _productRepository.Create(product);
        }

        public Task<Product> Update(int productId, Product product)
        {
            return _productRepository.Update(productId, product);
        }

        public void Delete(int productId)
        {
            _productRepository.Delete(productId);
        }
    }
}
