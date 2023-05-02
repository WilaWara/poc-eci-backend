using API.DTOs;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO productCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product newProduct = _mapper.Map<Product>(productCreateDTO);
            Product savedProduct = await _productService.Create(newProduct);

            var responseProduct = _mapper.Map<ProductResponseDTO>(savedProduct);
            return Ok(responseProduct);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(int productId, [FromBody] ProductCreateDTO productCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product updateData = _mapper.Map<Product>(productCreateDTO);
            Product updatedProduct = await _productService.Update(productId, updateData);

            var responseProduct = _mapper.Map<ProductResponseDTO>(updatedProduct);
            return Ok(responseProduct);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            return NoContent();
        }
    }
}
