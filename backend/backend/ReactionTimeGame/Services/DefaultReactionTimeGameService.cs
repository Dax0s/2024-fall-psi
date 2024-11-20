using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Settings;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.ReactionTimeGame.Services;

public class DefaultReactionTimeGameService : IReactionTimeGameService
{
    private readonly GamesDbContext _dbContext;

    public DefaultReactionTimeGameService(GamesDbContext dbContext)
        => _dbContext = dbContext;

    public WaitDuration NextWaitDuration()
        => new WaitDuration((new Random()).NextWithinBounds(GameSettings.WaitBounds));

    public async Task<List<ReactionTimeGameScore>> GetLeaderboardAsync(ushort numberOfScores)
        => await _dbContext
            .ReactionTimeGameScores
            .OrderByDescending(score => score.Value)
            .ThenBy(score => score.Date)
            .Take(numberOfScores)
            .ToListAsync().ConfigureAwait(false);

    public async Task AddScoreAsync(ReactionTimeGameScore newScore)
    {
        bool usernameExists = await _dbContext.ReactionTimeGameScores
            .AnyAsync(score => score.Username == newScore.Username).ConfigureAwait(false);

        if (usernameExists)
        {
            return;
        }

        await _dbContext.ReactionTimeGameScores.AddAsync(newScore).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
