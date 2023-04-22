using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetAsync(Guid id);

        Task<Order> AddAsync(Order order);

        Task<Order> DeleteAsync(Guid id);

        Task<Order> UpdateAsync(Guid id, Order order);

        Task<Order_Products> AddProductsAsync(Guid orderId, Order_Products newOrderProduct);

        Task<Order_Products> RemoveProductsAsync(Guid orderId, Guid productId);
    }
}
