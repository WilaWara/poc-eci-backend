using Application.CQRS.Commands.Category;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.CQRS.Handlers.Category
{
    public class CreateCategoryHandler
        : IRequestHandler<CreateCategoryCommand, Domain.Entities.Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Domain.Entities.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Domain.Entities.Category()
            {
                Name = request.name
            };

            return await _categoryRepository.Create(newCategory);
        }
    }
}
