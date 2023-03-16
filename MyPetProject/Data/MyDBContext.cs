using Microsoft.EntityFrameworkCore;
using MyPetProject.Models.Domain;

namespace MyPetProject.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
