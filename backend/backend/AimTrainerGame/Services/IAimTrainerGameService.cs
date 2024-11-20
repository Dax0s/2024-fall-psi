using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Settings;

namespace backend.AimTrainerGame.Services;

public interface IAimTrainerGameService
{
    public Task<(List<DotInfo>, DifficultySettings)> StartGame(GameStartRequest gameInfo);
    public Task<Highscore> EndGame(GameEndRequest gameInfo);
    public Task<IEnumerable<Highscore>> GetHighscores(int amount);
}
