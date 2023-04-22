namespace MyPetProject.Models.DTO
{
    public class Order_Products
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductCount { get; set; }

        //Navigation properties

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
