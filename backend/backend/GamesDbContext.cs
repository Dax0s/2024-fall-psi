using backend.AimTrainerGame.Models;
using backend.DotCountGame.Models;
using backend.ReactionTimeGame.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class GamesDbContext(DbContextOptions<GamesDbContext> options) : DbContext(options)
{
    public DbSet<Highscore> AimTrainerGameHighscores { get; set; }
    public DbSet<ReactionTimeGameScore> ReactionTimeGameScores { get; set; }
    public DbSet<DotCountGameScore> DotCountGameScores { get; set; }
}
