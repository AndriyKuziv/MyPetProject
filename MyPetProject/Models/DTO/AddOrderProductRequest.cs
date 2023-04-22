namespace MyPetProject.Models.DTO
{
    public class AddOrderProductRequest
    {
        public Guid productId { get; set; }
        public int ProductCount { get; set; }
    }
}
