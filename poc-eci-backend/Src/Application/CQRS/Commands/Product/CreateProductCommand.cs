using MediatR;

namespace Application.CQRS.Commands.Product
{
    public record CreateProductCommand(
        string Name,
        string Photo,
        string Description,
        int Stock,
        decimal Price,
        DateTime ExpireDate,
        int CategoryId
    ) : IRequest<Domain.Entities.Product>;
}
