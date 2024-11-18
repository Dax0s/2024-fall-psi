using backend.AimTrainerGame.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class GamesDbContext(DbContextOptions<GamesDbContext> options) : DbContext(options)
{
    public DbSet<Highscore> AimTrainerGameHighscores { get; set; }
}
