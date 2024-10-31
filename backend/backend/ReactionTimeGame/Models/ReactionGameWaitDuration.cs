using backend.ReactionTimeGame.Settings;
using backend.Utils;

namespace backend.ReactionTimeGame.Models;

public class ReactionGameWaitDuration
{
    public int MillisecondsToWait { get; init; }

    public ReactionGameWaitDuration()
        => MillisecondsToWait = (new Random()).NextWithinIntBounds(GameSettings.WaitBounds);
}
