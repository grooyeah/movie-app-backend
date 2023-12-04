using Database;
using Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthServiceImpl(UserDbContext userDbContext,IConfiguration configuration)
        {
            _dbContext = userDbContext;
            _configuration = configuration;
        }

        public async Task<User> LogOut(UserDto user)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username );
        }

        public async Task<string> SignIn(string email, string password)
        {
            //Add user password hashing and checking

            var existingUser = await _dbContext.Profiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(x => x.User.Email == email);

            if(existingUser == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, existingUser.User.PasswordHash, existingUser.User.PasswordSalt))
            {
                return null;
            }

            var token = CreateToken(existingUser.User);

            return token;
        }

        

        public async Task<Profile> SignUp(ProfileDto profileDto)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == profileDto.User.Email );
            if (existingUser != null)
            {
                return null;
            }

            var profile = profileDto.ToProfile();
            CreatePasswordHash(profileDto.User.Password, out byte[] passwordHash, out byte[] passwordSalt);
            profile.User.PasswordHash = passwordHash;
            profile.User.PasswordSalt = passwordSalt;

            profile.User.UserId = Guid.NewGuid().ToString();
            profile.ProfileId = Guid.NewGuid().ToString();
            foreach(var review in profile.Reviews)
            {
                review.ReviewId = Guid.NewGuid().ToString();
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
