using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Settings;

namespace backend.AimTrainerGame.Services;

public interface IAimTrainerGameService
{
    public (List<DotInfo>, DifficultySettings) StartGame(GameStartRequest gameInfo);
}
