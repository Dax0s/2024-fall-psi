using backend.DotCountGame.Data;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;

namespace backend.DotCountGame.Services;

public interface IDotCountGameService
{
    public DotCountCanvas GenerateNextCanvas(IDotCanvasGenerator canvasGenerator, int maxDotCount);
    public List<DotCountGameScore> GetLeaderboard(ushort numberOfScores);
    public void AddScore(DotCountGameScore newScore);
}
