using API.DTOs;
using Application.CQRS.Commands.Product;
using Application.CQRS.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Service;
using MediatR;
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
        private readonly IMediator _mediator;

        public ProductController(
            IProductService productService, 
            IMapper mapper,
            IMediator mediator)
        {
            _productService = productService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllProductQuery query = new GetAllProductQuery();
            var products = await _mediator.Send(query);
            //var products = await _productService.GetAll(); //To use with Service
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
            //Product savedProduct = await _productService.Create(newProduct); //To use with Service
            CreateProductCommand query = new CreateProductCommand(
                newProduct.Name,
                newProduct.Photo,
                newProduct.Description,
                newProduct.Stock,
                newProduct.Price,                
                newProduct.ExpireDate,
                newProduct.CategoryId
            );
            Product savedProduct = await _mediator.Send(query);

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
            //Product updatedProduct = await _productService.Update(productId, updateData); //To use with Service
            UpdateProductCommand query = new UpdateProductCommand(productId, updateData);
            Product updatedProduct = await _mediator.Send(query);

            var responseProduct = _mapper.Map<ProductResponseDTO>(updatedProduct);
            return Ok(responseProduct);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            DeleteProductCommand query = new DeleteProductCommand(productId);
            _mediator.Send(query);
            //_productService.Delete(productId); //To use with Service
            return NoContent();
        }
    }
}
