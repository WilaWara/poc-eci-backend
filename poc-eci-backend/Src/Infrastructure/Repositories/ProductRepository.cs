using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _db;

        public ProductRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return _db.Products
                .Where(p => p.Enabled == true)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Product? GetById(int productId)
        {
            return _db.Products.FirstOrDefault(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetByFilters(string? name, string? category, decimal? minPrice, decimal? maxPrice)
        {
            return _db.Products
                .Where(p => p.Name.Contains(name))
                .OrderBy(p => p.Id)
                .ToList();
        }

        public async Task<Product> Create(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("The product doesn't exist");
            }

            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            existingProduct.ExpireDate = product.ExpireDate;
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Stock = product.Stock;
            _db.Products.Update(existingProduct);
            _db.SaveChanges();
            return existingProduct;
        }

        public void Delete(int productId)
        {
            var existingProduct = GetById(productId);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("The product doesn't exist");
            }

            existingProduct.Enabled = false;
            _db.Products.Update(existingProduct);
            _db.SaveChanges();
        }
    }
}
