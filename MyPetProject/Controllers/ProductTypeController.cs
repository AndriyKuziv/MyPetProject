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
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeController(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var productTypes = _productTypeRepository.GetAllAsync();

            var productTypesDTO = _mapper.Map<List<Models.DTO.ProductType>>(productTypes);

            return Ok(productTypesDTO);
        }

        [HttpGet]
        [Route("id:guid")]
        [ActionName("GetProductType")]
        public async Task<IActionResult> GetProductType(Guid id)
        {
            var productType = await _productTypeRepository.GetAsync(id);

            if (productType is null) return NotFound();

            var productTypeDTO = _mapper.Map<Models.DTO.ProductType>(productType);

            return Ok(productTypeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(Models.DTO.AddProductTypeRequest addProductTypeRequest)
        {
            var productType = new Models.Domain.ProductType()
            {
                Name = addProductTypeRequest.Name
            };

            productType = await _productTypeRepository.AddAsync(productType);

            var productTypeDTO = _mapper.Map<Models.DTO.ProductType>(productType);

            return Ok(productTypeDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProductType(Guid id)
        {
            var productType = await _productTypeRepository.DeleteAsync(id);

            if (productType is null) return NotFound();

            var productTypeDTO = _mapper.Map<Models.DTO.ProductType>(productType);

            return Ok(productTypeDTO);
        }

        [HttpPut]
        [Route("id:guid")]
        public async Task<IActionResult> UpdateProductType([FromRoute] Guid id, 
            [FromBody] Models.DTO.UpdateProductTypeRequest updateProductTypeRequest)
        {
            throw new NotImplementedException();
        }
    }
}
