using backend.Properties;

namespace backend.ReactionTimeGame.Models;

public class ReactionGameWaitDuration
{
    public int MillisecondsToWait { get; set; }

    public ReactionGameWaitDuration()
        => MillisecondsToWait = Settings.ReactionTimeGame.Wait.Random();
}
