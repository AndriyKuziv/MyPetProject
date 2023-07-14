using Microsoft.EntityFrameworkCore;
using MyPetProject.Data;
using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public class OrderStatusRepository
    {
        private readonly MyDBContext _dbContext;

        public OrderStatusRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return await _dbContext.OrderStatus.ToListAsync();
        }

        public async Task<OrderStatus> GetAsync(Guid id)
        {
            return await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Id == id);
        }

        public async Task<OrderStatus> GetByNameAsync(string statusName)
        {
            return await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Name == statusName);
        }

        public async Task<OrderStatus> AddAsync(OrderStatus orderStatus)
        {
            orderStatus.Id = Guid.NewGuid();

            await _dbContext.AddAsync(orderStatus);
            await _dbContext.SaveChangesAsync();

            return orderStatus;
        }

        public async Task<OrderStatus> DeleteAsync(Guid id)
        {
            var orderStatus = await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Id == id);

            if (orderStatus is null) return null;

            _dbContext.Remove(orderStatus);
            await _dbContext.SaveChangesAsync();

            return orderStatus;
        }

        public async Task<OrderStatus> UpdateAsync(Guid id, OrderStatus orderStatus)
        {
            var existingOrderStatus = await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Id == id);

            if (existingOrderStatus is null) return null;

            existingOrderStatus.Name = orderStatus.Name;

            await _dbContext.SaveChangesAsync();

            return existingOrderStatus;
        }
    }
}
