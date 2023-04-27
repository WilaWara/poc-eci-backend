using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = _mapper.Map<Category>(categoryCreateDTO);
            var savedCategory = _categoryService.Create(newCategory);

            var responseCategory = _mapper.Map<CategoryResponseDTO>(savedCategory);
            return CreatedAtAction("Successfully created", new { id = responseCategory.Id }, responseCategory);
        }
    }
}
