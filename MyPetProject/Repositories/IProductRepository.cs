using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetByNameAsync(string name);


        Task<Product> AddAsync(Product product);
        
        Task<Product> DeleteAsync(Guid id);

        Task<Product> UpdateAsync(Guid id, Product product);
    }
}
