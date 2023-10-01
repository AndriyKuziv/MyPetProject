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

        // Get all orders
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Order
                .Include(x => x.OrderStatus)
                .Include(x => x.User)
                .ToListAsync();
        }

        // Get order by its ID
        public async Task<Order> GetAsync(Guid id)
        {
            return await _dbContext.Order
                .Include(x => x.OrderStatus)
                .Include(x => x.User)
                .FirstOrDefaultAsync(or => or.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllByUserAsync(Guid id)
        {
            return await _dbContext.Order
                .Include(x => x.OrderStatus)
                .Include(x => x.User)
                .Where(order => order.UserId == id).ToListAsync();
        }

        // Get products that an order contains
        public async Task<IEnumerable<OrderProduct>> GetOrderProductsAsync(Guid id)
        {
            return await _dbContext.OrderProduct
                .Where(op => op.OrderId == id)
                .Include(x => x.Product)
                .ToListAsync();
        }

        // Add a new order
        public async Task<Order> AddAsync(Order order)
        {
            order.Id = Guid.NewGuid();

            // adds default status to an order
            var defaultStatus = "Pending";
            var orderStatus = await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Name.ToLower() == defaultStatus.ToLower());

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

        // Delete an order
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

        // Update status of an order
        public async Task<Order> UpdateOrderStatusAsync(Guid OrderId, string statusName)
        {
            var order = await GetAsync(OrderId);
            if (order is null)
            {
                return null;
            }

            var newOrderStatus = await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Name.ToLower() == statusName.ToLower());
            if (newOrderStatus is null)
            {
                return null;
            }

            order.OrderStatusId = newOrderStatus.Id;
            await _dbContext.SaveChangesAsync();

            return order;
        }

        // Add new product

        // Remove product
    }
}
