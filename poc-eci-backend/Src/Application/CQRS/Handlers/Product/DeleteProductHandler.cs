using Application.CQRS.Commands.Product;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.Handlers.Product
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _productRepository.Delete(request.producId);
        }
    }
}
