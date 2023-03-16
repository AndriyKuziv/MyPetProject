namespace MyPetProject.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Guid ProductTypeId { get; set; }

        // Navigation property
        public ProductType ProductType { get; set; }
    }
}
