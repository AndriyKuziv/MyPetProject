using MyPetProject.Data;
using MyPetProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MyPetProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDBContext _dbContext;

        public ProductRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Product
                .Include(x => x.ProductType)
                .ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _dbContext.Product
                .Include(x => x.ProductType)
                .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.Id = Guid.NewGuid();

            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteAsync(Guid id)
        {
            var product = await _dbContext.Product.FirstOrDefaultAsync(pr => pr.Id == id);

            if (product is null) return null;

            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await _dbContext.Product.FirstOrDefaultAsync(pr => pr.Id == id);

            if (existingProduct is null) return null;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.ProductTypeId = product.ProductTypeId;
            
            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
