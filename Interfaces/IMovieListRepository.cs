using Models;

namespace Interfaces
{
    public interface IMovieListRepository
    {
        Task<ICollection<MovieList>> GetMovieListByProfileIdAsync(string profileId);
        Task<MovieList> CreateMovieListAsync(MovieList movieList);
        Task<MovieList> UpdateMovieListAsync(MovieList movieList);
        Task<bool> DeleteMovieListAsync(string movieListId);
        Task AddMovieToMovieListAsync(string movieListId, string imbdId);
        Task RemoveMovieFromMovieListAsync(string movieListId, string imbdId);
    }
}