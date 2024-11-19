using backend.AimTrainerGame.Models;
using backend.MathGame.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class GamesDbContext(DbContextOptions<GamesDbContext> options) : DbContext(options)
{
    public DbSet<Highscore> AimTrainerGameHighscores { get; set; }

    public DbSet<Puzzle> Puzzles { get; set; }
}
