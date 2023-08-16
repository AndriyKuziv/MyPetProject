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
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            var ordersDTO = _mapper.Map<List<Models.DTO.Order>>(orders);

            return Ok(ordersDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var order = await _orderRepository.GetAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            var orderProducts = await _orderRepository.GetOrderProductsAsync(id);

            order.OrderProducts = (List<Models.Domain.OrderProduct>)orderProducts;

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpGet]
        [Route("{id:guid}/products")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetOrderProducts([FromRoute]Guid id)
        {
            var orderProducts = await _orderRepository.GetOrderProductsAsync(id);

            if (orderProducts is null)
            {
                return NotFound();
            }

            var orderProductsDTO = _mapper.Map<List<Models.DTO.OrderProduct>>(orderProducts);

            return Ok(orderProductsDTO);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddOrder([FromBody]Models.DTO.AddOrderRequest addOrderRequest)
        {
            var order = new Models.Domain.Order()
            {
                OrderProducts = _mapper.Map<List<Models.Domain.OrderProduct>>(addOrderRequest.OrderProducts),
                UserId = new Guid("D2ABC02B-4785-4B7E-9C03-6741F8CECD12")
            };

            order = await _orderRepository.AddAsync(order);

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder([FromRoute]Guid id)
        {
            var order = await _orderRepository.DeleteAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("{orderId:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProductToOrder([FromRoute] Guid orderId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{orderId:guid}/{productId:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductFromOrder()
        {
            throw new NotImplementedException();
        }
    }
}
