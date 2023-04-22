namespace MyPetProject.Models.DTO
{
    public class AddOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid OrderStatusId { get; set; }
    }
}
