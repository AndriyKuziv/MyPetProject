using MyPetProject.Models.Domain;
using System.Runtime.InteropServices;

namespace MyPetProject.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
