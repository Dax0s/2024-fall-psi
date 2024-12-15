using System;
using backend.MathGame.Models;
using Xunit;

namespace testing.MathGame.Models;

public class PuzzleTests
{
    [Fact]
    public void Construction()
    {
        var (id, content) = (Guid.NewGuid(), "content");
        var highscore = new Puzzle
        {
            Id = id,
            Content = content
        };

        Assert.Equal(id, highscore.Id);
        Assert.Equal(content, highscore.Content);
    }
}
