using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adăugăm EF Core InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MoviesDb"));

// Adăugăm repository-ul
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Activăm controllerele
builder.Services.AddControllers();

// Activăm Swagger (opțional pentru testare)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware standard
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// ✅ Seed de date (2 filme)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Movies.Any())
    {
        context.Movies.AddRange(
            new Movie
            {
                Title = "Inception",
                Genre = "Sci-Fi",
                Description = "A dream inside a dream.",
                ImageUrl = "https://image.tmdb.org/t/p/original/qmDpIHrmpJINaRKAfWQfftjCdyi.jpg"
            },
            new Movie
            {
                Title = "The Dark Knight",
                Genre = "Action",
                Description = "Legend of the Dark Knight.",
                ImageUrl = "https://image.tmdb.org/t/p/original/qJ2tW6WMUDux911r6m7haRef0WH.jpg"
            }
        );
        context.SaveChanges();
    }
}

app.Run();
