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
            return await _dbContext.Order
                .Include(x => x.User)
                .Include(x => x.OrderStatus)
                .Include(x => x.Products)
                .ToListAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await _dbContext.Order
                .Include(x => x.User)
                .Include(x => x.OrderStatus)
                .Include(x => x.Products)
                .FirstOrDefaultAsync(or => or.Id == id);
        }

        public async Task<IEnumerable<OrderProduct>> GetOrderProductsAsync(Guid id)
        {
            return await _dbContext.OrderProduct
                .Where(op => op.OrderId == id)
                .Include(x => x.Product)
                .ToListAsync();
        }
        
        public async Task<Order> AddAsync(Order order)
        {
            order.Id = Guid.NewGuid();

            // add default status to the order
            var defaultStatus = "Pending";
            var orderStatus = await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Name == defaultStatus);

            if (orderStatus is null)
            {
                throw new Exception("Default order status is not existing.");
            }

            order.OrderStatusId = orderStatus.Id;

            await _dbContext.AddAsync(order);

            foreach(var product in order.OrderProducts)
            {
                product.Id = Guid.NewGuid();
                product.OrderId = order.Id;
                await _dbContext.AddAsync(product);
            }

            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> DeleteAsync(Guid id)
        {
            var order = await _dbContext.Order.FirstOrDefaultAsync(or => or.Id == id);

            if (order is null)
            {
                return null;
            }

            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        // Add new product

        // Remove product
    }
}
