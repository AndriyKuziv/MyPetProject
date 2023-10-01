using MyPetProject.Data;
using MyPetProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace MyPetProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext dbContext;

        public UserRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get the list of all users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.User
                .ToListAsync();
        }

        // Get user by their ID
        public async Task<User> GetAsync(Guid id)
        {
            return await dbContext.User
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        // Get user by their name (currently not working)
        public async Task<User> GetByNameAsync(string name)
        {
            return await dbContext.User
                .FirstOrDefaultAsync(user => user.Username == name);
        }

        // Add new user
        public async Task<User> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();

            // adding new user to database
            await this.dbContext.AddAsync(user);

            // add "user" status to a new user
            string defaultRole = "user";
            Role dbRole = await dbContext.Role.FirstOrDefaultAsync(role => role.Name == defaultRole);

            if (dbRole is null)
            {
                throw new Exception("There is no 'user' role in database.");
            }

            User_Role user_Role = new User_Role()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = dbRole.Id
            };

            await dbContext.UserRole.AddAsync(user_Role);
            await dbContext.SaveChangesAsync();
            
            return user;
        }

        // Authenticate a user by their user name and password
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await dbContext.User
                .FirstOrDefaultAsync(usr => usr.Username.ToLower() == username.ToLower() && usr.Password == password);

            if (user is null)
            {
                return null;
            }

            var userRoles = await dbContext.UserRole.Where(ur => ur.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userRole in userRoles)
                {
                    var role = await dbContext.Role.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }

        // Delete user by their ID
        public async Task<User> DeleteAsync(Guid id)
        {
            var user = await dbContext.User.FirstOrDefaultAsync(user => user.Id == id);

            if (user is null) return null;

            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        // Update user by their ID
        public async Task<User> UpdateAsync(Guid id, User user)
        {
            var existingUser = await dbContext.User.FirstOrDefaultAsync(user => user.Id == id);

            if (existingUser is null) return null;

            existingUser.Username = user.Username;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
