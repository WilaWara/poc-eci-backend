using Application.CQRS.Queries;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.Handlers.Product
{
    public class GetByFiltersProductHandler : IRequestHandler<GetByFiltersProductQuery, IEnumerable<Domain.Entities.Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetByFiltersProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetByFiltersProductQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByFilters(
                request.name,
                request.category,
                request.minPrice,
                request.maxPrice
            );
        }
    }
}
