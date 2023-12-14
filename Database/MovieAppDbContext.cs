using Microsoft.EntityFrameworkCore;
using Models;

namespace Database
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }
        
        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
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
            optionsBuilder.EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.UserId);
            modelBuilder.Entity<Profile>().HasKey(profile => profile.ProfileId);
            modelBuilder.Entity<Review>().HasKey(review => review.ReviewId);
            modelBuilder.Entity<MovieList>().HasKey(movieList => movieList.MovieListId);

            ConfigureForeignKeys(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureForeignKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne<Profile>() // Assuming Profile is the name of the entity with MProfileId
                .WithMany()
                .HasForeignKey(r => r.RProfileId) // Explicitly specify the foreign key property
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MovieList>()
                .HasOne<Profile>() // Assuming Profile is the name of the entity with MProfileId
                .WithMany()
                .HasForeignKey(ml => ml.MProfileId) // Explicitly specify the foreign key property
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

