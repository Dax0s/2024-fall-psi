using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Testcontainers.PostgreSql;
using Xunit;

using backend;
using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Services;
using backend.ReactionTimeGame.Settings;

namespace testing.ReactionTimeGame.Services;

public class DefaultReactionTimeGameServiceTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    private GamesDbContext _context;
    private DefaultReactionTimeGameService _service;
    private readonly IMemoryCache _memoryCache;

    public DefaultReactionTimeGameServiceTests()
    {
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("math_game_test_db")
            .WithUsername("test_user")
            .WithPassword("test_pass")
            .Build();

        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync().ConfigureAwait(true);

        var options = new DbContextOptionsBuilder<GamesDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .Options;

        _context = new GamesDbContext(options);
        await _context.Database.MigrateAsync().ConfigureAwait(true);

        //_context.Puzzles.Add(new Puzzle { Content = "1 + 1" });
        await _context.SaveChangesAsync().ConfigureAwait(true);

        _service = new DefaultReactionTimeGameService(_context);
    }

    public async Task DisposeAsync()
    {
        if (_context != null)
        {
            await _context.Database.EnsureDeletedAsync().ConfigureAwait(true);
            await _context.DisposeAsync().ConfigureAwait(true);
        }
        await _dbContainer.DisposeAsync().ConfigureAwait(true);
    }

    [Fact]
    public void NextWaitDuration()
    {
        var nextWaitDuration = _service.NextWaitDuration();
        Assert.InRange(
            nextWaitDuration.MillisecondsToWait,
            GameSettings.WaitBounds.LowerLimit,
            GameSettings.WaitBounds.UpperLimit
        );
    }

    [Fact]
    public async Task GetLeaderboardAsync()
    {
        var user1 = new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = "user1",
            Value = 100,
            Date = DateTime.UtcNow,
        };
        var user2 = new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = "user2",
            Value = 110,
            Date = DateTime.UtcNow,
        };

        await _context.ReactionTimeGameScores.AddAsync(user1).ConfigureAwait(true);
        await _context.ReactionTimeGameScores.AddAsync(user2).ConfigureAwait(true);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        var topScores = await _service.GetLeaderboardAsync(2).ConfigureAwait(true);

        Assert.Equal(2, topScores.Count);
        Assert.True(
            topScores[0].Value > topScores[1].Value
            ||
            (topScores[0].Value == topScores[1].Value && topScores[0].Date <= topScores[1].Date)
        );
    }

}
