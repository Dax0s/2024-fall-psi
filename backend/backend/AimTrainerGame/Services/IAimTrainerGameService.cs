using backend.AimTrainerGame.Models;

namespace backend.AimTrainerGame.Services;

public interface IAimTrainerGameService
{
    public List<DotInfo> StartGame(GameStartRequest gameInfo, out int amountOfDots, out int timeToLive);
}
