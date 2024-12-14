using backend.DotCountGame.Data;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;
using backend.DotCountGame.Settings;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.DotCountGame.Services;

public class DefaultDotCountGameService : IDotCountGameService
{
    private readonly GamesDbContext _dbContext;

    public DefaultDotCountGameService(GamesDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<DotCountCanvas> GenerateNextCanvas(IDotCanvasGenerator canvasGenerator, int maxDotCount)
    {
        return await Task.Run(() =>
            canvasGenerator.GenerateNextCanvas(
                new Bounds<int>(GameSettings.DotCount.LowerLimit, maxDotCount)
            )
        ).ConfigureAwait(false);
    }

    public async Task<List<DotCountGameScore>> GetLeaderboard(ushort numberOfScores)
    {
        return await _dbContext
            .DotCountGameScores
            .OrderByDescending(score => score.Value)
            .ThenBy(score => score.Date)
            .Take(numberOfScores)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> AddScore(DotCountGameScore newScore)
    {
        var existingScore = await _dbContext.DotCountGameScores
            .FirstOrDefaultAsync(score => score.Username == newScore.Username)
            .ConfigureAwait(false);

        if (existingScore != null)
        {
            return false;
        }

        await _dbContext.AddAsync(newScore).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}
