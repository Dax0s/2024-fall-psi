using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Settings;

namespace backend.AimTrainerGame.Services;

public class AimTrainerGameService() : IAimTrainerGameService
{
    public (List<DotInfo>, DifficultySettings) StartGame(GameStartRequest gameInfo)
    {
        var difficultySettings = GameSettings.GetDifficultySettings(gameInfo.difficulty);

        var random = new Random();
        var dotInfoList = Enumerable
            .Range(0, difficultySettings.dotCount)
            .Select(_ => random.NextDotInfo(gameInfo.screenSize, difficultySettings.spawnTime))
            .ToList();

        return (dotInfoList, difficultySettings);
    }
}
