using MyPetProject.Profiles;

namespace MyPetProject.Models.DTO
{
    public class AddOrderRequest
    {
        public string Address { get; set; }
        public List<AddOrderProductRequest> OrderProducts { get; set; }
    }
}
