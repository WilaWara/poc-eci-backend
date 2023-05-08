using Application.CQRS.Commands.Category;
using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Queries;

namespace API.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IMediator mediator)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            var responseCategories = new List<CategoryResponseDTO>();
            foreach (var category in categories)
            {
                var responseCategory = _mapper.Map<CategoryResponseDTO>(category);
                responseCategories.Add(responseCategory);
            }
            return Ok(responseCategories);
        }

        //To use with CQRS Handlers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateCategoryCommand query = new CreateCategoryCommand(categoryCreateDTO.Name);
            Category savedCategory = await _mediator.Send(query);

            //Category newCategory = _mapper.Map<Category>(categoryCreateDTO); //To use with Services
            //Category savedCategory = await _categoryService.Create(newCategory); //To use with Services

            var responseCategory = _mapper.Map<CategoryResponseDTO>(savedCategory);
            return Ok(responseCategory);
        }
    }
}
