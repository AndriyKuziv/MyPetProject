using MyPetProject.Data;
using MyPetProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace MyPetProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext _dbContext;

        public UserRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.User
                .ToListAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _dbContext.User
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<IEnumerable<User>> GetByNameAsync(string name)
        {
            return await _dbContext.User
                .Where(user => user.Username.ToLower().Contains(name.ToLowerInvariant()))
                .ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();

            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> DeleteAsync(Guid id)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(user => user.Id == id);

            if (user is null) return null;

            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _dbContext.User.FirstOrDefaultAsync(user => user.Id == id);

            if (existingUser is null) return null;

            existingUser.Username = user.Username;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await _dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
