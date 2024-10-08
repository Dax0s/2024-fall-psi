namespace backend.ReactionTimeGame.Models;

public class ReactionTime
{
    public int MillisecondsToWait { get; set; }

    // TODO: extract constants somewhere else more global
    private const int MinWaitInMs = 2000;
    private const int MaxWaitInMs = 5000;

    public ReactionTime()
    {
        MillisecondsToWait = MinWaitInMs <= MaxWaitInMs
            ? Random.Shared.Next(MinWaitInMs, MaxWaitInMs)
            : 0;
    }
}
