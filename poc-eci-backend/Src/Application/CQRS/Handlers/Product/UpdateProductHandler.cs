using Application.CQRS.Commands.Product;
using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;
using System.Drawing;
using XAct.Library.Settings;

namespace Application.CQRS.Handlers.Product
{
    public class UpdateProductHandler
        : IRequestHandler<UpdateProductCommand, Domain.Entities.Product>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Domain.Entities.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.Update(request.productId, request.product);
        }
    }
}
