using backend.Utils;

namespace backend.ReactionTimeGame.Models;

public class ReactionGameWaitDuration
{
    public int MillisecondsToWait { get; set; }

    public ReactionGameWaitDuration()
        => MillisecondsToWait = Random.Shared.Next(
                Constants.ReactionTimeGame.Wait.Min,
                Constants.ReactionTimeGame.Wait.Max + 1
            );
}
