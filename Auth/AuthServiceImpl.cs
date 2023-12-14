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
        private readonly MovieAppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthServiceImpl(MovieAppDbContext dbContext,IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<bool> LogOut(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId) != null;
        }

        public async Task<UserDto> Login(string username, string password)
        {
            //Add user password hashing and checking

            var existingUser = await _dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == username);

            if(existingUser == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                return null;
            }

            _dbContext.Users.Entry(existingUser).State = EntityState.Detached;

            return existingUser.ToUserDto();
        }

        public async Task<UserDto> SignUp(SignUpModel signUpModel)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == signUpModel.Username);

            if (existingUser != null)
            {
                return null;
            }

            var user = new User()
            {
                Username = signUpModel.Username
            };

            CreatePasswordHash(signUpModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.UserId = Guid.NewGuid().ToString();

            user.Email = signUpModel.Email;
            
            var profile = new Profile()
            {
                ProfileId = Guid.NewGuid().ToString(),
                Reviews = new List<Review>(),
                UserId = user.UserId,
                Picture = "",
                MovieLists = new List<MovieList>()
            };

            var profileEmtpyReview = new Review()
            {
                ReviewId = Guid.NewGuid().ToString(),
                RProfileId = profile.ProfileId,
                ImdbID = "tt0993846",
                Author = user.Username,
                MovieTitle = "The Wolf of Wall Street",
                ReviewText = "Favorite Movie",
                Rating = 5,
                PublishedOn = DateTime.UtcNow,
                ReviewTitle = "Review Title"
            };

            var profileEmptyMovieList = new MovieList()
            {
                MovieListId = Guid.NewGuid().ToString(),
                MProfileId = profile.ProfileId,
                ListName = "New list",
                ListDescription = "New list",
                ImbdIds = new List<string>()
            };

            profile.Reviews.Add(profileEmtpyReview);
            profile.MovieLists.Add(profileEmptyMovieList);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.Profiles.AddAsync(profile);
            await _dbContext.Reviews.AddAsync(profileEmtpyReview);
            await _dbContext.MovieLists.AddAsync(profileEmptyMovieList);

            await _dbContext.SaveChangesAsync();

            var createdUser = await _dbContext.Users.FirstOrDefaultAsync(x => user.UserId == x.UserId);

            if (createdUser == null)
            {
                return null;
            }

            return createdUser.ToUserDto();
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
