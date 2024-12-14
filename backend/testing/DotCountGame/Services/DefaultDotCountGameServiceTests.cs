using System;
using System.Threading.Tasks;
using backend;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;
using backend.DotCountGame.Services;
using backend.DotCountGame.Settings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace testing.DotCountGame.Services;

public class DefaultDotCountGameServiceTests
{
    private readonly GamesDbContext _dbContext;
    private readonly DefaultDotCountGameService _service;

    public DefaultDotCountGameServiceTests()
    {
        var options = new DbContextOptionsBuilder<GamesDbContext>()
            .UseInMemoryDatabase(databaseName: "TestGamesDatabase")
            .Options;
        _dbContext = new GamesDbContext(options);
        _service = new DefaultDotCountGameService(_dbContext);
    }

    [Fact]
    public async Task GenerateNextCanvas()
    {
        var dotCanvasGenerator = new DefaultDotCanvasGenerator();
        var maxDotCount = 39;

        for (uint i = 0; i < 5; ++i)
        {
            var canvas = await _service.GenerateNextCanvas(dotCanvasGenerator, maxDotCount).ConfigureAwait(false);

            Assert.InRange(canvas.Dots.Count, GameSettings.DotCount.LowerLimit, GameSettings.DotCount.UpperLimit);

            foreach (var dot in canvas.Dots)
            {
                Assert.InRange(
                    dot.Center.X,
                    0 + dot.Radius,
                    canvas.SideLength - dot.Radius
                );
                Assert.InRange(
                    dot.Center.Y,
                    0 + dot.Radius,
                    canvas.SideLength - dot.Radius
                );
            }
        }
    }

    [Fact]
    public async Task GetLeaderboardAsync()
    {
        var score1 = new DotCountGameScore
        {
            Id = Guid.NewGuid(),
            Username = "user1",
            Value = 100,
            Date = DateTime.UtcNow,
        };
        var score2 = new DotCountGameScore
        {
            Id = Guid.NewGuid(),
            Username = "user2",
            Value = 110,
            Date = DateTime.UtcNow,
        };

        await _service.AddScore(score1).ConfigureAwait(true);
        await _service.AddScore(score2).ConfigureAwait(true);
        var topScores = await _service.GetLeaderboard(2).ConfigureAwait(true);

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
        var score = new DotCountGameScore
        {
            Id = Guid.NewGuid(),
            Username = "username",
            Value = 100,
            Date = DateTime.UtcNow,
        };
        Assert.True(await _service.AddScore(score).ConfigureAwait(true));
        Assert.False(await _service.AddScore(score).ConfigureAwait(true));
    }

}
