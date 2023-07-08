using MyPetProject.Profiles;

namespace MyPetProject.Models.DTO
{
    public class AddOrderRequest
    {
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
