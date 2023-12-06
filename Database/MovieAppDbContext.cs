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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User to Profile (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

            // Profile to Review (One-to-Many)
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Profile)
                .HasForeignKey(r => r.ProfileId);

            // Profile to MovieList (One-to-Many)
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.MovieLists)
                .WithOne(ml => ml.Profile)
                .HasForeignKey(ml => ml.ProfileId);
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

