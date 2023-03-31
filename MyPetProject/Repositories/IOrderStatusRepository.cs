using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync();

        Task<OrderStatus> GetAsync(Guid id);

        Task<OrderStatus> AddAsync(OrderStatus orderStatus);

        Task<OrderStatus> DeleteAsync(Guid id);

        Task<OrderStatus> UpdateAsync(Guid id, OrderStatus orderStatus);
    }
}
