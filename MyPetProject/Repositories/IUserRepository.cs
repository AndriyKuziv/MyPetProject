using MyPetProject.Models.Domain;

namespace MyPetProject.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetAsync(Guid id);

        Task<IEnumerable<User>> GetByNameAsync(string name);

        Task<User> AuthenticateAsync(string username, string password);

        Task<User> AddAsync(User user);

        Task<User> DeleteAsync(Guid id);

        Task<User> UpdateAsync(Guid id, User user);
    }
}
