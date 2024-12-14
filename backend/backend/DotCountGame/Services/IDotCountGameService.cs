using backend.DotCountGame.Data;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;

namespace backend.DotCountGame.Services;

public interface IDotCountGameService
{
    Task<DotCountCanvas> GenerateNextCanvas(IDotCanvasGenerator canvasGenerator, int maxDotCount);
    Task<List<DotCountGameScore>> GetLeaderboard(ushort numberOfScores);
    Task<bool> AddScore(DotCountGameScore newScore);
}
