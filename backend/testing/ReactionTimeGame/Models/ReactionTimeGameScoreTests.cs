using System;
using Xunit;

using backend.ReactionTimeGame.Models;

namespace testing.ReactionTimeGame.Models;

public class ReactionTimeGameScoreTests
{
    [Fact]
    public void Construction()
    {
        var (id, username, value, date) = (Guid.NewGuid(), "username", 69, DateTime.UtcNow);
        var score = new ReactionTimeGameScore
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
