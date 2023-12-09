using Microsoft.EntityFrameworkCore;
using Models;

namespace Database
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5433;Database=movie-app-db;Username=postgres;Password=postgres;",
                builder => builder.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null
                )
            );
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.UserId);
            modelBuilder.Entity<Profile>().HasKey(profile => profile.ProfileId);
            modelBuilder.Entity<Review>().HasKey(review => review.ReviewId);
            modelBuilder.Entity<MovieList>().HasKey(movieList => movieList.MovieListId);
            
            modelBuilder.Entity<Review>()
                .HasOne<Profile>() // Assuming Profile is the name of the entity with ProfileId
                .WithMany()
                .HasForeignKey(r => r.ProfileId) // Explicitly specify the foreign key property
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MovieList>()
                .HasOne<Profile>() // Assuming Profile is the name of the entity with ProfileId
                .WithMany()
                .HasForeignKey(ml => ml.ProfileId) // Explicitly specify the foreign key property
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}

