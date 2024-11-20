using backend.DotCountGame.Data;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;

namespace backend.DotCountGame.Services;

public interface IDotCountGameService
{
    Task<DotCountCanvas> GenerateNextCanvasAsync(IDotCanvasGenerator canvasGenerator, int maxDotCount);
    Task<List<DotCountGameScore>> GetLeaderboardAsync(ushort numberOfScores);
    Task AddScoreAsync(DotCountGameScore newScore);
}
