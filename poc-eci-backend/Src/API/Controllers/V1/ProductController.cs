using API.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            var responseProducts = new List<ProductResponseDTO>();
            foreach (var product in products)
            {
                var responseProduct = _mapper.Map<ProductResponseDTO>(product);
                responseProducts.Add(responseProduct);
            }
            return Ok(responseProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO productCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct = _mapper.Map<Product>(productCreateDTO);
            var savedProduct = _productService.Create(newProduct);

            var responseProduct = _mapper.Map<ProductResponseDTO>(savedProduct);
            return CreatedAtAction("Successfully created", new { id = responseProduct.Id }, responseProduct);
        }
    }
}
