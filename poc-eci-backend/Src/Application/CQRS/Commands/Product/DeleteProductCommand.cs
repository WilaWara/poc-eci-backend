using MediatR;

namespace Application.CQRS.Commands.Product
{
    public record DeleteProductCommand(int producId) : IRequest;
}
