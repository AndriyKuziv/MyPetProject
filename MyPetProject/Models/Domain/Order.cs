namespace MyPetProject.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderStatusId { get; set; }

        //Navigation properties
        public User User { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
