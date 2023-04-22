using Microsoft.EntityFrameworkCore;
using MyPetProject.Data;
using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDBContext _dbContext;

        public OrderRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Order.ToListAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await _dbContext.Order.FirstOrDefaultAsync(or => or.Id == id);
        }
        public async Task<Order> AddAsync(Order order)
        {
            order.Id = Guid.NewGuid();

            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> DeleteAsync(Guid id)
        {
            var order = await _dbContext.Order.FirstOrDefaultAsync(or => or.Id == id);

            if (order is null) return null;

            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateAsync(Guid id, Order order)
        {
            var existingOrder = await _dbContext.Order.FirstOrDefaultAsync(or => or.Id == id);

            if (existingOrder is null) return null;

            existingOrder.OrderStatusId = order.OrderStatusId;
            // existingOrder.UserId = order.UserId != null ? order.UserId: existingOrder.UserId;

            await _dbContext.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<Order_Products> AddProductsAsync(Guid orderId, 
            Order_Products newOrderProduct)
        {
            var order = await _dbContext.Order.FirstOrDefaultAsync(or => or.Id == orderId);
            if (order is null) return null;

            var product = await _dbContext.Product
                .FirstOrDefaultAsync(pr => pr.Id == newOrderProduct.ProductId);
            if (product is null) return null;

            var orderProduct = new Order_Products()
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = newOrderProduct.ProductId,
                ProductCount = newOrderProduct.ProductCount
            };

            await _dbContext.AddAsync(orderProduct);
            await _dbContext.SaveChangesAsync();

            return orderProduct;
        }

        public async Task<Order_Products> RemoveProductsAsync(Guid orderId, Guid productId)
        {
            var orderProduct = await _dbContext.Order_Products
                .FirstOrDefaultAsync(op => op.ProductId == productId && op.OrderId == orderId);
            if (orderProduct is null) return null;

            _dbContext.Remove(orderProduct);
            await _dbContext.SaveChangesAsync();

            return orderProduct;
        }
    }
}
