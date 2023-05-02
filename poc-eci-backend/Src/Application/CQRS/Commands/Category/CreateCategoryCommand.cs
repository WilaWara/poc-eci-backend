using MediatR;

namespace Application.CQRS.Commands.Category
{
    public record CreateCategoryCommand(string name) : IRequest<Domain.Entities.Category>;
}
