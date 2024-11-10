using Microsoft.EntityFrameworkCore;

namespace backend.AimTrainerGame.Models;

public class AimTrainerGameHighscoreContext(DbContextOptions<AimTrainerGameHighscoreContext> options) : DbContext(options)
{
    public DbSet<Highscore> Highscores { get; set; }
}
