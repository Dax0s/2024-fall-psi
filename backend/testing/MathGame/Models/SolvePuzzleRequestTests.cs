using backend.MathGame.Models;
using Xunit;

namespace testing.MathGame.Models;

public class SolvePuzzleRequestTests
{
    [Fact]
    public void Construction()
    {
        var (puzzle, answer) = ("puzzle", "answer");
        var highscore = new SolvePuzzleRequest
        {
            Puzzle = puzzle,
            Answer = answer
        };

        Assert.Equal(puzzle, highscore.Puzzle);
        Assert.Equal(answer, highscore.Answer);
    }
}
