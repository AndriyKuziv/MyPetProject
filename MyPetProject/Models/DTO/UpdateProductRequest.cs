namespace MyPetProject.Models.DTO
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Guid ProductTypeId { get; set; }
    }
}
