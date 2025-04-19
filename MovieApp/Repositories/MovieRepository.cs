using MovieApp.Models;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;

namespace MovieApp.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Movie>> GetLatestAsync() => await _context.Movies.OrderByDescending(m => m.Id).Take(10).ToListAsync();
        public async Task<IEnumerable<Movie>> SearchAsync(string name, string genre) =>
            await _context.Movies.Where(m => m.Title.Contains(name) || m.Genre.Contains(genre)).ToListAsync();
        public async Task<Movie> GetByIdAsync(int id) => await _context.Movies.Include(m => m.Comments).FirstOrDefaultAsync(m => m.Id == id);
        public async Task AddCommentAsync(int movieId, Comment comment)
        {
            comment.MovieId = movieId;
            comment.DatePosted = DateTime.UtcNow;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}