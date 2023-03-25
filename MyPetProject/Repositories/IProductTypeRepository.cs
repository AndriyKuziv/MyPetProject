using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllAsync();

        Task<ProductType> GetAsync(Guid id);

        Task<ProductType> AddAsync(ProductType productType);

        Task<ProductType> DeleteAsync(Guid id);

        Task<ProductType> UpdateAsync(Guid id, ProductType productType);
    }
}
