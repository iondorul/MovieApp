using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Repositories;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _repo;
    public MoviesController(IMovieRepository repo) => _repo = repo;

    [HttpGet("latest")] public async Task<IActionResult> GetLatest() => Ok(await _repo.GetLatestAsync());
    [HttpGet("search")] public async Task<IActionResult> Search(string name, string genre) => Ok(await _repo.SearchAsync(name, genre));
    [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _repo.GetByIdAsync(id));
    [HttpPost("{id}/comment")] public async Task<IActionResult> AddComment(int id, Comment c) { await _repo.AddCommentAsync(id, c); return Ok(); }
}