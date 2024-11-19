using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Settings;
using backend.Utils;

namespace backend.ReactionTimeGame.Services;

public class DefaultReactionTimeGameService : IReactionTimeGameService
{
    private readonly GamesDbContext _dbContext;

    public DefaultReactionTimeGameService(GamesDbContext dbContext)
        => _dbContext = dbContext;

    public WaitDuration NextWaitDuration()
        => new WaitDuration((new Random()).NextWithinBounds(GameSettings.WaitBounds));

    public List<ReactionTimeGameScore> GetLeaderboard(ushort numberOfScores)
        => _dbContext
            .ReactionTimeGameScores
            .OrderByDescending(score => score.Value)
            .ThenBy(score => score.Date)
            .Take(numberOfScores)
            .ToList();

    public void AddScore(ReactionTimeGameScore newScore)
    {
        foreach (ReactionTimeGameScore score in _dbContext.ReactionTimeGameScores)
        {
            if (newScore.Username == score.Username)
            {
                return;
            }
        }

        _dbContext.ReactionTimeGameScores.Add(newScore);
        _dbContext.SaveChanges();
    }
}
