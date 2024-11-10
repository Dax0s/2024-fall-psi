using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Settings;

namespace backend.AimTrainerGame.Services;

public interface IAimTrainerGameService
{
    public (List<DotInfo>, DifficultySettings) StartGame(GameStartRequest gameInfo);
}
