using Interfaces;
using Models;

namespace Services
{
    public class MovieListServiceImpl : IMovieListService
    {
        private readonly IMovieListRepository _movieListRepository;
        
        public MovieListServiceImpl(IMovieListRepository movieListRepository)
        {
            _movieListRepository = movieListRepository;
        }
        
        public async Task<ICollection<MovieList>> GetMovieListByProfileIdAsync(string profileId)
        {
            return await _movieListRepository.GetMovieListByProfileIdAsync(profileId);
        }
        
        public async Task<MovieList> CreateMovieListAsync(MovieList movieList)
        {
            movieList.MovieListId = Guid.NewGuid().ToString();
            return await _movieListRepository.CreateMovieListAsync(movieList);
        }
    
        public async Task<MovieList> UpdateMovieListAsync(MovieList movieList)
        {
            return await _movieListRepository.UpdateMovieListAsync(movieList);
        }
    
        public async Task<bool> DeleteMovieListAsync(string movieListId)
        {
            return await _movieListRepository.DeleteMovieListAsync(movieListId);
        }
    
        public async Task AddMovieToMovieListAsync(string movieListId, string imbdId)
        {
            await _movieListRepository.AddMovieToMovieListAsync(movieListId, imbdId);
        }
    
        public async Task RemoveMovieFromMovieListAsync(string movieListId, string imbdId)
        {
            await _movieListRepository.RemoveMovieFromMovieListAsync(movieListId, imbdId);
        }
    }
}

