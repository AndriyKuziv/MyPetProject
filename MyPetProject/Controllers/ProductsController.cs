using MyPetProject.Models.Domain;
using MyPetProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using MyPetProject.Models.DTO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace MyPetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _productRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();

            var productsDTO = _mapper.Map<List<MyPetProject.Models.DTO.Product>>(products);

            return Ok(productsDTO);
        }

        [HttpGet]
        [Route("id:guid")]
        [ActionName("GetProduct")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product is null) return NotFound();

            var productDTO = _mapper.Map<Models.DTO.Product>(product);

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Models.DTO.AddProductRequest addProductRequest)
        {
            var product = new Models.Domain.Product()
            {
                Name = addProductRequest.Name,
                Price = addProductRequest.Price,
                ProductTypeId = addProductRequest.ProductTypeId
            };

            product = await _productRepository.AddAsync(product);

            var productDTO = _mapper.Map<Models.DTO.Product>(product);

            return Ok(productDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productRepository.DeleteAsync(id);

            if (product is null) return NotFound();

            var productDTO = _mapper.Map<Models.DTO.Product>(product);

            return Ok(productDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, 
            [FromBody] Models.DTO.UpdateProductRequest updateProductRequest)
        {
            var product = new Models.Domain.Product()
            {
                Name = updateProductRequest.Name,
                Price = updateProductRequest.Price,
                ProductTypeId = updateProductRequest.ProductTypeId
            };

            var updatedProduct = await _productRepository.UpdateAsync(id, product);

            if (updatedProduct is null) return NotFound();

            var productDTO = _mapper.Map<Models.DTO.Product>(updatedProduct);

            return Ok(productDTO);
        }
    }
}
