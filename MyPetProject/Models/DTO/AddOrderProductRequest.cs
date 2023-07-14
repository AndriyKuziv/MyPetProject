namespace MyPetProject.Models.DTO
{
    public class AddOrderProductRequest
    {
        public Guid ProductId { get; set; }
        public int ProductCount { get; set; }
    }
}
