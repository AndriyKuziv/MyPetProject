using MyPetProject.Models.Domain;
using MyPetProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using MyPetProject.Models.DTO;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.ComponentModel;

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
        [Route("{orderId:guid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);

            if (order is null)
            {
                return NotFound();
            }

            if (!await HasAccess(order.UserId.ToString()))
            {
                return Forbid();
            }

            var orderProducts = await _orderRepository.GetOrderProductsAsync(orderId);

            order.OrderProducts = (List<Models.Domain.OrderProduct>)orderProducts;

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpGet]
        [Route("userOrders/{userId:guid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetUserOrders([FromRoute] Guid userId)
        {

        }

        [HttpGet]
        [Route("{orderId:guid}/products")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetOrderProducts([FromRoute]Guid orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);

            if (order is null)
            {
                return NotFound();
            }

            if (!await HasAccess(order.UserId.ToString()))
            {
                return Forbid();
            }

            var orderProducts = await _orderRepository.GetOrderProductsAsync(orderId);

            if (orderProducts is null)
            {
                return NotFound();
            }

            var orderProductsDTO = _mapper.Map<List<Models.DTO.OrderProduct>>(orderProducts);

            return Ok(orderProductsDTO);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateOrder([FromBody]Models.DTO.AddOrderRequest addOrderRequest)
        {
            var order = new Models.Domain.Order()
            {
                UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                OrderProducts = _mapper.Map<List<Models.Domain.OrderProduct>>(addOrderRequest.OrderProducts),
                FirstName = User.FindFirstValue(ClaimTypes.Name),
                LastName = User.FindFirstValue(ClaimTypes.Surname),
                Address = addOrderRequest.Address
            };

            order = await _orderRepository.AddAsync(order);

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpPut]
        [Route("{orderId:guid}/updateStatus")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid orderId, [FromBody] string orderStatus)
        {
            var order = await _orderRepository.UpdateOrderStatusAsync(orderId, orderStatus);

            if (order is null) return NotFound();

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpDelete]
        [Route("{orderId:guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder([FromRoute]Guid orderId)
        {
            var order = await _orderRepository.DeleteAsync(orderId);

            if (order is null)
            {
                return NotFound();
            }

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("{orderId:guid}/addProduct")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProductToOrder([FromRoute] Guid orderId, [FromBody] Guid productId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{orderId:guid}/deleteProduct")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductFromOrder()
        {
            throw new NotImplementedException();
        }

        // Check whether the user is trying to get their own order or are they an admin
        private async Task<bool> HasAccess(string userIdOrder)
        {
            var userIdJwt = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.FindAll(ClaimTypes.Role).Where(role => role.Value.ToLower() == "admin").Count() >= 1;
            if (userIdJwt != userIdOrder && !isAdmin)
            {
                return false;
            }

            return true;
        }
    }
}
