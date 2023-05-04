using Domain.Entities;
using Domain.Interfaces.Repository;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class CategoryRepositoryShould
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRepositoryShould()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationDBContext(options);
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public async Task Create_WhenCalled_ShouldCreateCategory()
        {
            // Arrange
            var category = new Category
            {
                Name = "Test Category"
            };

            // Act
            var result = await _categoryRepository.Create(category);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Category", result.Name);
            Assert.True(result.Id > 0);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
