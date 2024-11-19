using backend.DotCountGame.Data;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;
using backend.DotCountGame.Settings;
using backend.Utils;

namespace backend.DotCountGame.Services;

public class DefaultDotCountGameService : IDotCountGameService
{
    private readonly GamesDbContext _dbContext;

    public DefaultDotCountGameService(GamesDbContext dbContext)
        => _dbContext = dbContext;

    public DotCountCanvas GenerateNextCanvas(IDotCanvasGenerator canvasGenerator, int maxDotCount)
        => canvasGenerator.GenerateNextCanvas(
            new Bounds<int>(GameSettings.DotCount.LowerLimit, maxDotCount)
        );

    public List<DotCountGameScore> GetLeaderboard(ushort numberOfScores)
        => _dbContext
                .DotCountGameScores
                .OrderByDescending(score => score.Value)
                .ThenBy(score => score.Date)
                .Take(numberOfScores)
                .ToList();

    public void AddScore(DotCountGameScore newScore)
    {
        foreach (DotCountGameScore score in _dbContext.DotCountGameScores)
        {
            if (newScore.Username == score.Username)
            {
                return;
            }
        }

        _dbContext.Add(newScore);
        _dbContext.SaveChanges();
    }
}
