using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using movie_app_backend.Exceptions;

namespace Repository
{
    public class MovieListRepositoryImpl : IMovieListRepository
    {
        private readonly UserDbContext _dbContext;
    
        public MovieListRepositoryImpl(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<ICollection<MovieList>> GetMovieListByProfileIdAsync(string profileId)
        {
            var movieList = await _dbContext.MovieLists.Where(m => m.ProfileId == profileId).ToListAsync();

            if (movieList == null)
            {
                throw new NotFoundException($"MovieList with ProfileId '{profileId}' not found.");
            }
            
            return movieList;
        }
    
        public async Task<MovieList> CreateMovieListAsync(MovieList movieList)
        {
            _dbContext.MovieLists.Add(movieList);
            await _dbContext.SaveChangesAsync();
    
            return movieList;
        }
    
        public async Task<MovieList> UpdateMovieListAsync(MovieList movieList)
        {
            var existingMovieList = await _dbContext.MovieLists.FirstOrDefaultAsync(m => m.ProfileId == movieList.ProfileId);
    
            if (existingMovieList == null)
            {
                throw new NotFoundException($"MovieList with ProfileId '{movieList.ProfileId}' not found.");
            }
    
            _dbContext.MovieLists.Update(movieList);
    
            await _dbContext.SaveChangesAsync();
    
            return existingMovieList;
        }
    
        public async Task<bool> DeleteMovieListAsync(string movieListId)
        {
            var movieList = await _dbContext.MovieLists.FirstOrDefaultAsync(m => m.MovieListId == movieListId);
    
            if (movieList == null)
            {
                return false;
            }
    
            _dbContext.MovieLists.Remove(movieList);
            await _dbContext.SaveChangesAsync();
    
            return true;
        }
    
        public async Task AddMovieToMovieListAsync(string movieListId, string imbdId)
        {
            var movieList = await _dbContext.MovieLists.FirstOrDefaultAsync(m => m.MovieListId == movieListId);
    
            if (movieList == null)
            {
                throw new NotFoundException($"MovieList with UserId '{movieListId}' not found.");
            }
    
            if (movieList.ImbdIds.Contains(imbdId))
            {
                throw new ArgumentException($"Movie with ID '{imbdId}' already exists in the movie list.");
            }
    
            movieList.ImbdIds.Add(imbdId);
            await _dbContext.SaveChangesAsync();
        }
    
        public async Task RemoveMovieFromMovieListAsync(string movieListId, string imbdId)
        {
            var movieList = await _dbContext.MovieLists.FirstOrDefaultAsync(m => m.MovieListId == movieListId);
    
            if (movieList == null)
            {
                throw new NotFoundException($"MovieList with UserId '{movieListId}' not found.");
            }
    
            if (!movieList.ImbdIds.Remove(imbdId))
            {
                throw new ArgumentException($"Movie with ID '{imbdId}' does not exist in the movie list.");
            }
    
            await _dbContext.SaveChangesAsync();
        }
    }
}

