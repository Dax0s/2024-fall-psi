using backend.ReactionTimeGame.Models;

namespace backend.ReactionTimeGame.Services;

public interface IReactionTimeGameService
{
    public WaitDuration NextWaitDuration();
    public List<ReactionTimeGameScore> GetLeaderboard(ushort numberOfScores);
    public void AddScore(ReactionTimeGameScore newScore);
}
