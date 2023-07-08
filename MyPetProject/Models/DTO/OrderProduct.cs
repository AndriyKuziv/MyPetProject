namespace MyPetProject.Models.DTO
{
    public class OrderProduct
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int ProductCount { get; set; }

        // Navigation property
        public Product Product { get; set; }
    }
}
