using MyPetProject.Models.Domain;

namespace MyPetProject.Models.DTO
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderStatusId { get; set; }

        //Navigation properties
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
