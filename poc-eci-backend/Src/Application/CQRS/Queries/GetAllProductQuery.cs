using MediatR;

namespace Application.CQRS.Queries
{
    public record GetAllProductQuery()
        : IRequest<IEnumerable<Domain.Entities.Product>>;
}
