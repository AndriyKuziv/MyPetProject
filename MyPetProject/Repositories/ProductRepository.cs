using MyPetProject.Data;
using MyPetProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyPetProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateAsync(Guid id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
