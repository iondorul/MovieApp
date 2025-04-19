using MovieApp.Models;

namespace MovieApp.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetLatestAsync();
        Task<IEnumerable<Movie>> SearchAsync(string name, string genre);
        Task<Movie> GetByIdAsync(int id);
        Task AddCommentAsync(int movieId, Comment comment);
    }
}