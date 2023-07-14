using MyPetProject.Profiles;

namespace MyPetProject.Models.DTO
{
    public class AddOrderRequest
    {
        public List<AddOrderProductRequest> OrderProducts { get; set; }
    }
}
