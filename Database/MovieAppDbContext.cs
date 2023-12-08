using Microsoft.EntityFrameworkCore;
using Models;
using Polly;
using System.Data.Common;

namespace Database
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }

        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=movie-app-db;Username=postgres;Password=postgres;",
                builder => builder.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null
                )
            );
        }
    }
}

