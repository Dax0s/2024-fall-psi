using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

using backend;
using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Services;
using backend.ReactionTimeGame.Settings;

namespace testing.ReactionTimeGame.Services;

public class DefaultReactionTimeGameServiceTests //: IAsyncLifetime
{
    private readonly GamesDbContext _dbContext;
    private readonly DefaultReactionTimeGameService _service;

    public DefaultReactionTimeGameServiceTests()
    {
        var options = new DbContextOptionsBuilder<GamesDbContext>()
            .UseInMemoryDatabase(databaseName: "TestGamesDatabase")
            .Options;
        _dbContext = new GamesDbContext(options);
        _service = new DefaultReactionTimeGameService(_dbContext);
    }

    [Fact]
    public void NextWaitDuration()
    {
        Assert.InRange(
            _service.NextWaitDuration().MillisecondsToWait,
            GameSettings.WaitBounds.LowerLimit,
            GameSettings.WaitBounds.UpperLimit
        );
    }

    [Fact]
    public async Task GetLeaderboardAsync()
    {
        var score1 = new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = "user1",
            Value = 100,
            Date = DateTime.UtcNow,
        };
        var score2 = new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = "user2",
            Value = 110,
            Date = DateTime.UtcNow,
        };

        await _service.AddScoreAsync(score1).ConfigureAwait(true);
        await _service.AddScoreAsync(score2).ConfigureAwait(true);
        var topScores = await _service.GetLeaderboardAsync(2).ConfigureAwait(true);

        Assert.Equal(2, topScores.Count);
        Assert.True(
            topScores[0].Value > topScores[1].Value
            ||
            (topScores[0].Value == topScores[1].Value && topScores[0].Date <= topScores[1].Date)
        );
    }

    [Fact]
    public async Task AddScoreAsync()
    {
        var score = new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = "username",
            Value = 100,
            Date = DateTime.UtcNow,
        };
        Assert.True(await _service.AddScoreAsync(score).ConfigureAwait(true));
        Assert.False(await _service.AddScoreAsync(score).ConfigureAwait(true));
    }
}
