namespace backend.ReactionTimeGame.Models;

public class ReactionTime
{
    public int MillisecondsToWait { get; set; }

    // TODO: extract constants somewhere else more global
    private const int MIN_WAIT_IN_MS = 2000;
    private const int MAX_WAIT_IN_MS = 5000;

    public ReactionTime()
    {
        MillisecondsToWait = MIN_WAIT_IN_MS <= MAX_WAIT_IN_MS
            ? Random.Shared.Next(MIN_WAIT_IN_MS, MAX_WAIT_IN_MS)
            : 0;
    }
}
