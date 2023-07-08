using System.ComponentModel.DataAnnotations.Schema;

namespace MyPetProject.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderStatusId { get; set; }

        [NotMapped]
        public List<OrderProduct> OrderProducts { get; set; }

        //Navigation properties
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<Product> Products { get; set; }
    }
}
