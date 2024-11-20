using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Settings;

namespace backend.AimTrainerGame.Services;

public interface IAimTrainerGameService
{
    public Task<(List<DotInfo>, DifficultySettings)> StartGameAsync(GameStartRequest gameInfo);
    public Task<Highscore> EndGameAsync(GameEndRequest gameInfo);
    public Task<IEnumerable<Highscore>> GetHighscoresAsync(int amount);
}
