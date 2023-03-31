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

            existingOrder.ProductId = order.ProductId;
            existingOrder.OrderStatusId = order.OrderStatusId;
            existingOrder.UserId = order.UserId;

            await _dbContext.SaveChangesAsync();

            return existingOrder;
        }
    }
}
