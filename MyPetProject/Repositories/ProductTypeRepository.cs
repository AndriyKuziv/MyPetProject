using Microsoft.EntityFrameworkCore;
using MyPetProject.Data;
using MyPetProject.Models.Domain;
using System.Net.WebSockets;

namespace MyPetProject.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly MyDBContext _dbContext;

        public ProductTypeRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            return await _dbContext.ProductType.ToListAsync();
        }

        public async Task<ProductType> GetAsync(Guid id)
        {
            return await _dbContext.ProductType.FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task<ProductType> AddAsync(ProductType productType)
        {
            productType.Id = Guid.NewGuid();

            await _dbContext.AddAsync(productType);
            await _dbContext.SaveChangesAsync();

            return productType;
        }

        public async Task<ProductType> DeleteAsync(Guid id)
        {
            var productType = await _dbContext.ProductType.FirstOrDefaultAsync(pt => pt.Id == id);

            if (productType is null) return null;

            _dbContext.Remove(productType);
            await _dbContext.SaveChangesAsync();

            return productType;
        }

        public async Task<ProductType> UpdateAsync(Guid id, ProductType productType)
        {
            var existingProductType = await _dbContext.ProductType.FirstOrDefaultAsync(pt => pt.Id == id);

            if (existingProductType is null) return null;

            existingProductType.Name = productType.Name;

            await _dbContext.SaveChangesAsync();

            return existingProductType;
        }
    }
}
