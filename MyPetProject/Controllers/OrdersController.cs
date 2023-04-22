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
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            var ordersDTO = _mapper.Map<List<Models.DTO.Order>>(orders);

            return Ok(ordersDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);

            if (order is null) return NotFound();

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Models.DTO.AddOrderRequest addOrderRequest)
        {
            var order = new Models.Domain.Order()
            {
                OrderStatusId = addOrderRequest.OrderStatusId,
                UserId = addOrderRequest.UserId
            };

            order = await _orderRepository.AddAsync(order);

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _orderRepository.DeleteAsync(id);

            if (order is null) return NotFound();

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateOrder([FromRoute]Guid id, 
            [FromBody] Models.DTO.UpdateOrderRequest updateOrderRequest)
        {
            var order = new Models.Domain.Order()
            {
                OrderStatusId = updateOrderRequest.OrderStatusId
            };
 
            order = await _orderRepository.UpdateAsync(id, order);

            if (order is null) return NotFound();

            var orderDTO = _mapper.Map<Models.DTO.Order>(order);

            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("{orderId:guid}")]
        public async Task<IActionResult> AddProductToOrder([FromRoute] Guid orderId, 
            [FromBody] AddOrderProductRequest addOrderProductRequest)
        {
            var orderProduct = new Models.Domain.Order_Products()
            {
                OrderId = orderId,
                ProductId = addOrderProductRequest.productId,
                ProductCount = addOrderProductRequest.ProductCount
            };

            orderProduct = await _orderRepository.AddProductsAsync(orderId, orderProduct);
            
            if (orderProduct is null) return NotFound();

            var orderProductDTO = _mapper.Map<Models.DTO.Order_Products>(orderProduct);

            return Ok(orderProductDTO);
        }

        [HttpDelete]
        [Route("{orderId:guid}/{productId:guid}")]
        public async Task<IActionResult> DeleteProductFromOrder([FromRoute] Guid orderId, [FromRoute] Guid productId)
        {
            var orderProduct = await _orderRepository.RemoveProductsAsync(orderId, productId);

            if(orderProduct is null) return NotFound();

            var orderProductDTO = _mapper.Map<Models.DTO.Order_Products>(orderProduct);

            return Ok(orderProductDTO);
        }
    }
}
