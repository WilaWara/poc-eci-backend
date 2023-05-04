using API.Controllers.V1;
using API.DTOs;
using API.Mappers;
using Application.CQRS.Commands.Category;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace API.Tests.Controllers.V1
{
    public class CategoryControllerShould
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IMapper _mapper;

        public CategoryControllerShould()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryProfile());
            }).CreateMapper();
        }

        [Fact]
        public async Task Create_Returns_OkObjectResult_With_CategoryResponseDTO()
        {
            // Arrange
            var categoryCreateDTO = new CategoryCreateDTO { Name = "Test Category" };
            var savedCategory = new Domain.Entities.Category { Id = 1, Name = "Test Category" };
            var expectedCategoryResponseDTO = new CategoryResponseDTO { Id = 1, Name = "Test Category" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), default)).ReturnsAsync(savedCategory);
            var controller = new CategoryController(_mapper, _mediatorMock.Object);

            // Act
            var result = await controller.Create(categoryCreateDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var categoryResponseDTO = Assert.IsType<CategoryResponseDTO>(okResult.Value);
            Assert.Equal(expectedCategoryResponseDTO.Id, categoryResponseDTO.Id);
            Assert.Equal(expectedCategoryResponseDTO.Name, categoryResponseDTO.Name);
        }
    }
}
