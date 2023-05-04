using Application.CQRS.Commands.Category;
using Application.CQRS.Handlers.Category;
using Domain.Interfaces.Repository;
using Moq;
using Xunit;

namespace Application.Tests.CQRS.Handlers.Category
{
    public class CreateCategoryHandlerShould
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerShould()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _handler = new CreateCategoryHandler(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCreatedCategory()
        {
            // Arrange
            var request = new CreateCategoryCommand("Test Category 2") 
            { 
                name = "Test Category"
            };
            var expectedCategory = new Domain.Entities.Category() { Id = 1, Name = "Test Category" };
            _mockCategoryRepository.Setup(x => x.Create(It.IsAny<Domain.Entities.Category>())).ReturnsAsync(expectedCategory);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCategory.Id, result.Id);
            Assert.Equal(expectedCategory.Name, result.Name);
            _mockCategoryRepository.Verify(x => x.Create(It.IsAny<Domain.Entities.Category>()), Times.Once);
        }
    }
}
