using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using movie_app_backend.Exceptions;

namespace Repository
{
    public class MovieListRepositoryImpl : IMovieListRepository
    {
        private readonly MovieAppDbContext _dbContext;
    
        public MovieListRepositoryImpl(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<ICollection<MovieList>> GetMovieListByProfileIdAsync(string profileId)
        {
            var movieList = await _dbContext.MovieLists.Where(m => m.MProfileId == profileId).ToListAsync();

            if (movieList == null)
            {
                throw new NotFoundException($"MovieList with MProfileId '{profileId}' not found.");
            }
            
            return movieList;
        }
    
        public async Task<MovieList> CreateMovieListAsync(MovieList movieList)
        {
            await _dbContext.MovieLists.AddAsync(movieList);
            await _dbContext.SaveChangesAsync();
    
            return movieList;
        }
    
        public async Task<MovieList> UpdateMovieListAsync(MovieList movieList)
        {
            var movieListToUpdate = await _dbContext.MovieLists.FirstOrDefaultAsync(m => m.MProfileId == movieList.MProfileId);
    
            if (movieListToUpdate == null)
            {
                throw new NotFoundException($"MovieList with MProfileId '{movieList.MProfileId}' not found.");
            }

            movieListToUpdate.ListName = movieList.ListName;
            movieListToUpdate.ListDescription = movieList.ListDescription;
            movieListToUpdate.ImbdIds = movieList.ImbdIds;

            await _dbContext.SaveChangesAsync();
    
            return movieListToUpdate;
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

