using MediatR;

namespace Application.CQRS.Queries
{
    public record GetByFiltersProductQuery(
        string? name, 
        int? categoryId,
        decimal? minPrice, 
        decimal? maxPrice
    )
        : IRequest<IEnumerable<Domain.Entities.Product>>;
}
