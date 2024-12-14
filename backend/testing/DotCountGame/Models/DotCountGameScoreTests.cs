using System;
using Xunit;

using backend.DotCountGame.Models;

namespace testing.DotCountGame.Models;

public class DotCountGameScoreTests
{
    [Fact]
    public void Construction()
    {
        var (id, username, value, date) = (Guid.NewGuid(), "username", 69, DateTime.UtcNow);
        var score = new DotCountGameScore
        {
            Id = id,
            Username = username,
            Value = value,
            Date = date,
        };

        Assert.Equal(score.Id, id);
        Assert.Equal(score.Username, username);
        Assert.Equal(score.Value, value);
        Assert.Equal(score.Date, date);
    }
}
