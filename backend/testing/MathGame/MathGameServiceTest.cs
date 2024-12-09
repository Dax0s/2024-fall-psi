using System.Threading.Tasks;
using backend.MathGame.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Testcontainers.PostgreSql;
using Xunit;

namespace backend.MathGame.Tests;

public class MathGameServiceTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    private GamesDbContext _context;
    private MathGameService _service;
    private readonly IMemoryCache _memoryCache;

    public MathGameServiceTests()
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

        _context.Puzzles.Add(new Puzzle { Content = "1 + 1" });
        await _context.SaveChangesAsync().ConfigureAwait(true);

        _service = new MathGameService(_context, _memoryCache);
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
    public async Task GetNextPuzzleAsync_ReturnsPuzzle_WhenPuzzlesExist()
    {
        _context.Puzzles.Add(new Puzzle { Content = "1 + 1" });
        await _context.SaveChangesAsync().ConfigureAwait(true);

        var puzzle = await _service.GetNextPuzzleAsync().ConfigureAwait(false);

        Assert.NotNull(puzzle);
        Assert.Equal("1 + 1", puzzle);
    }

    [Theory]
    [InlineData("2 + 2", "4", true)]
    [InlineData("2 - 1", "1", true)]
    [InlineData("3 * 3", "9", true)]
    [InlineData("8 / 2", "4", true)]
    [InlineData("5 / 0", "0", false)]
    [InlineData("1 + 4", "1", false)]
    public async Task CheckAnswerAsync_ReturnsExpectedResult(string puzzle, string answer, bool expected)
    {
        var result = await _service.CheckAnswerAsync(puzzle, answer).ConfigureAwait(true);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckAnswerAsync_ReturnsFalse_WhenInvalidPuzzle()
    {
        var puzzle = "Invalid Puzzle";
        var answer = "0";

        var result = await _service.CheckAnswerAsync(puzzle, answer).ConfigureAwait(true);

        Assert.False(result);
    }
}
