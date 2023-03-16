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
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
