using MediatR;

namespace Application.CQRS.Commands.Product
{
    public record UpdateProductCommand(int productId, Domain.Entities.Product product)
        : IRequest<Domain.Entities.Product>;
}
