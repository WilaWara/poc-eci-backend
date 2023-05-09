using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data;
using XAct.Categorization;

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
                //.Where(p => p.Enabled == true) 
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Product? GetById(int productId)
        {
            return _db.Products.FirstOrDefault(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetByFilters(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            List<Product> products = new List<Product>();

            if ((name == "" || name == null) && 
                (categoryId == 0 || categoryId == null) && 
                (minPrice == 0 || minPrice == null) && 
                (maxPrice == 0 || maxPrice == null))
            {
                products = _db.Products.OrderBy(p => p.Id).ToList();
                return products;
            }

            if (name != null)
            {
                List<Product> filterName = _db.Products
                    .Where(c => c.Name
                    .ToLower()
                    .Contains(
                        name.ToLower().Trim()
                    ))
                    .ToList();

                if (products.Count() == 0)
                {
                    products.AddRange(filterName);
                }
                else
                {
                    List<Product> matchingProducts = products.Intersect(filterName).ToList();
                    products = matchingProducts;
                }
            }

            if (categoryId != null && categoryId != 0)
            {
                List<Product> filterCategory = _db.Products
                    .Where(c => c.CategoryId == categoryId)
                    .ToList();

                if (products.Count() == 0)
                {
                    products.AddRange(filterCategory);
                }
                else
                {
                    List<Product> matchingProducts = products.Intersect(filterCategory).ToList();
                    products = matchingProducts;
                }
            }

            if ((minPrice != null && minPrice != 0) && (maxPrice != null && maxPrice != 0))
            {
                List<Product> filterPrice = _db.Products
                    .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                    .ToList();

                if (products.Count() == 0)
                {
                    products.AddRange(filterPrice);
                }
                else
                {
                    List<Product> matchingProducts = products.Intersect(filterPrice).ToList();
                    products = matchingProducts;
                }
            }

            return products;
        }

        public async Task<Product> Create(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(int productId, Product product)
        {
            var existingProduct = GetById(productId);
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
