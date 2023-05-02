using Application.CQRS.Queries;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.Handlers.Product
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Domain.Entities.Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAll();
        }
    }
}
