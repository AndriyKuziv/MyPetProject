using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetAsync(Guid id);

        Task<IEnumerable<OrderProduct>> GetOrderProductsAsync(Guid id);

        Task<Order> AddAsync(Order order);

        Task<Order> UpdateOrderStatusAsync(Guid OrderId, string statusName);

        Task<Order> DeleteAsync(Guid id);
    }
}
