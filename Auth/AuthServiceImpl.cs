using Database;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Auth
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly UserDbContext _dbContext;

        public AuthServiceImpl(UserDbContext userDbContext)
        {
            _dbContext = userDbContext;
        }

        public async Task<User> LogOut(User user)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
        }

        public async Task<Profile> SignIn(User user)
        {
            return await _dbContext.Profiles.FirstOrDefaultAsync(x => x.User.Username == user.Username && x.User.Password == user.Password);
        }

        public async Task<Profile> SignUp(Profile profile)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == profile.User.Email );
            if (existingUser != null)
            {
                return null;
            }
            profile.User.UserId = Guid.NewGuid().ToString();
            profile.ProfileId = Guid.NewGuid().ToString();
            foreach(var review in profile.Reviews)
            {
                review.PublishedOn = DateTime.UtcNow;
            }
            await _dbContext.Users.AddAsync(profile.User);
            await _dbContext.Profiles.AddAsync(profile);
            await _dbContext.SaveChangesAsync();
            var createdProfile = await _dbContext.Profiles.FirstOrDefaultAsync(x => profile.User.Username == x.User.Username);
            if (createdProfile == null)
            {
                return null;
            }
            return createdProfile;
        }
    }
}
