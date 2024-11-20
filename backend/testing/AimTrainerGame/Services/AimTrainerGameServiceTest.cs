using System;
using System.Linq;
using System.Threading.Tasks;
using backend;
using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Services;
using backend.AimTrainerGame.Settings;
using backend.Utils;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace testing.AimTrainerGame.Services;

public class AimTrainerGameServiceTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    private GamesDbContext _context;
    private AimTrainerGameService _service;
    private readonly Vec2<int> _defaultScreenSize = new(1920, 1080);

    public AimTrainerGameServiceTests()
    {
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("test_db")
            .WithUsername("test_user")
            .WithPassword("test_pass")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync().ConfigureAwait(true);

        var options = new DbContextOptionsBuilder<GamesDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .Options;

        _context = new GamesDbContext(options);
        await _context.Database.MigrateAsync().ConfigureAwait(true);

        _service = new AimTrainerGameService(_context);
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

    [Theory]
    [InlineData(Difficulty.Easy, 10, 1500, 500, 1500)]
    [InlineData(Difficulty.Medium, 15, 1250, 250, 1250)]
    [InlineData(Difficulty.Hard, 20, 1000, 0, 1000)]
    public async Task StartGame_ShouldReturnCorrectSettingsForDifficulty(
        Difficulty difficulty,
        int expectedDotCount,
        int expectedTimeToLive,
        int expectedMinSpawnTime,
        int expectedMaxSpawnTime)
    {
        var gameInfo = new GameStartRequest(difficulty, _defaultScreenSize);
        var (dots, settings) = await _service.StartGame(gameInfo).ConfigureAwait(true);

        Assert.Equal(expectedDotCount, settings.dotCount);
        Assert.Equal(expectedTimeToLive, settings.timeToLive);
        Assert.Equal(new Bounds<int>(expectedMinSpawnTime, expectedMaxSpawnTime), settings.spawnTime);

        Assert.Equal(expectedDotCount, dots.Count);
        foreach (var dot in dots)
        {
            Assert.InRange(dot.Pos.X, 0, _defaultScreenSize.X);
            Assert.InRange(dot.Pos.Y, 0, _defaultScreenSize.Y);
            Assert.InRange(dot.SpawnTime, expectedMinSpawnTime, expectedMaxSpawnTime);
        }
    }

    [Fact]
    public async Task EndGame_ShouldSaveHighscore()
    {
        var gameInfo = new GameEndRequest("TestUser", 1000);
        var result = await _service.EndGame(gameInfo).ConfigureAwait(true);

        Assert.NotNull(result);
        Assert.Equal(gameInfo.Username, result.Username);
        Assert.Equal(gameInfo.Score, result.Score);
        Assert.True((DateTime.UtcNow - result.Date).TotalSeconds < 1);

        var savedHighscore = await _context.AimTrainerGameHighscores
            .FirstOrDefaultAsync(h => h.Id == result.Id).ConfigureAwait(true);

        Assert.NotNull(savedHighscore);
        Assert.Equal(gameInfo.Username, savedHighscore.Username);
        Assert.Equal(gameInfo.Score, savedHighscore.Score);
    }

    [Fact]
    public async Task GetHighscores_ShouldReturnOrderedHighscores()
    {
        var now = DateTime.UtcNow;
        var highscores = new[]
        {
            new Highscore { Id = Guid.NewGuid(), Username = "User1", Score = 100, Date = now.AddDays(-1) },
            new Highscore { Id = Guid.NewGuid(), Username = "User2", Score = 200, Date = now.AddDays(-2) },
            new Highscore { Id = Guid.NewGuid(), Username = "User3", Score = 200, Date = now }
        };

        await _context.AimTrainerGameHighscores.AddRangeAsync(highscores).ConfigureAwait(true);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        var result = (await _service.GetHighscores(3).ConfigureAwait(true)).ToList();

        Assert.Equal(3, result.Count);
        Assert.Equal(highscores[1].Id, result[0].Id);
        Assert.Equal(highscores[2].Id, result[1].Id);
        Assert.Equal(highscores[0].Id, result[2].Id);
    }

    [Fact]
    public async Task GetHighscores_WithLimitSmallerThanAvailable_ShouldReturnRequestedAmount()
    {
        var highscores = Enumerable.Range(0, 10)
            .Select(i => new Highscore
            {
                Id = Guid.NewGuid(),
                Username = $"User{i}",
                Score = i,
                Date = DateTime.UtcNow.AddDays(-i)
            })
            .ToArray();

        await _context.AimTrainerGameHighscores.AddRangeAsync(highscores).ConfigureAwait(true);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        var result = (await _service.GetHighscores(5).ConfigureAwait(true)).ToList();

        Assert.Equal(5, result.Count);
        for (var i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].Score >= result[i + 1].Score);
        }
    }
}
