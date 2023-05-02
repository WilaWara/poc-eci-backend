using Application.CQRS.Commands.Product;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.Handlers.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Domain.Entities.Product>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Domain.Entities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Domain.Entities.Product()
            {
                Name = request.Name,
                Photo = request.Photo,
                Description = request.Description,
                Stock = request.Stock,
                Price = request.Price,
                ExpireDate = request.ExpireDate,
                CategoryId = request.CategoryId,
                Enabled = true
            };

            return await _productRepository.Create(newProduct);
        }
    }
}
