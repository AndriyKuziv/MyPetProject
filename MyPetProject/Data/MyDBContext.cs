using Microsoft.EntityFrameworkCore;
using MyPetProject.Models.Domain;

namespace MyPetProject.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<User_Role> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
