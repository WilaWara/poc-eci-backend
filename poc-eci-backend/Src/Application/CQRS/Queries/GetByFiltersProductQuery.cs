using MediatR;

namespace Application.CQRS.Queries
{
    public record GetByFiltersProductQuery(
        string? name, 
        string? category,
        decimal? minPrice, 
        decimal? maxPrice
    )
        : IRequest<IEnumerable<Domain.Entities.Product>>;
}
