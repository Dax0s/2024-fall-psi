using System;
using backend.AimTrainerGame.Models;
using Xunit;

namespace testing.AimTrainerGame.Model;

public class HighscoreTests
{
    [Fact]
    public void Construction()
    {
        var (id, username, score, date) = (Guid.NewGuid(), "username", 420, DateTime.UtcNow);
        var highscore = new Highscore
        {
            Id = id,
            Username = username,
            Score = score,
            Date = date
        };

        Assert.Equal(id, highscore.Id);
        Assert.Equal(username, highscore.Username);
        Assert.Equal(score, highscore.Score);
        Assert.Equal(date, highscore.Date);
    }
}
