using backend.ReactionTimeGame.Models;

namespace backend.ReactionTimeGame.Services;

public interface IReactionTimeGameService
{
    public WaitDuration NextWaitDuration();
    public Task<List<ReactionTimeGameScore>> GetLeaderboardAsync(ushort numberOfScores);
    public Task AddScoreAsync(ReactionTimeGameScore newScore);
}
