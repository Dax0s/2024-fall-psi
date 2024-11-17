namespace backend.ReactionTimeGame.Models;

public struct ReactionTime
{
    public int Milliseconds { get; init; }

    public ReactionTime(int milliseconds)
        => Milliseconds = milliseconds;
}
