namespace backend.MathGame.Models;

public record SolvePuzzleRequest
{
    public required string Puzzle { get; init; }
    public required string Answer { get; init; }
}
