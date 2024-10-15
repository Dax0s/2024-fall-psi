using backend.AimTrainerGame.Models;
using backend.Properties;
using backend.Utils;

namespace backend.AimTrainerGame.Services;

public class AimTrainerGameService() : IAimTrainerGameService
{
    private static int GetRandomSpawnTime(Settings.AimTrainerGame.Difficulty difficulty)
    {
        return difficulty switch
        {
            Settings.AimTrainerGame.Difficulty.Easy => Settings.AimTrainerGame.Easy.SpawnTime.Random(),
            Settings.AimTrainerGame.Difficulty.Medium => Settings.AimTrainerGame.Medium.SpawnTime.Random(),
            Settings.AimTrainerGame.Difficulty.Hard => Settings.AimTrainerGame.Hard.SpawnTime.Random(),
            _ => Settings.AimTrainerGame.Defaults.SpawnTime.Random(),
        };
    }

    private static int GetAmountOfDots(Settings.AimTrainerGame.Difficulty difficulty)
    {
        return difficulty switch
        {
            Settings.AimTrainerGame.Difficulty.Easy => Settings.AimTrainerGame.Easy.Dots,
            Settings.AimTrainerGame.Difficulty.Medium => Settings.AimTrainerGame.Medium.Dots,
            Settings.AimTrainerGame.Difficulty.Hard => Settings.AimTrainerGame.Hard.Dots,
            _ => Settings.AimTrainerGame.Defaults.Dots,
        };
    }

    private static int GetTimeToLive(Settings.AimTrainerGame.Difficulty difficulty)
    {
        return difficulty switch
        {
            Settings.AimTrainerGame.Difficulty.Easy => Settings.AimTrainerGame.Easy.TimeToLive,
            Settings.AimTrainerGame.Difficulty.Medium => Settings.AimTrainerGame.Medium.TimeToLive,
            Settings.AimTrainerGame.Difficulty.Hard => Settings.AimTrainerGame.Hard.TimeToLive,
            _ => Settings.AimTrainerGame.Defaults.TimeToLive
        };
    }

    public List<DotInfo> StartGame(GameStartRequest gameInfo, out int amountOfDots, out int timeToLive)
    {
        amountOfDots = GetAmountOfDots(gameInfo.difficulty);
        timeToLive = GetTimeToLive(gameInfo.difficulty);

        var random = new Random();
        return Enumerable.Range(0, amountOfDots)
            .Select(_ => new DotInfo(
                random.NextOffset(gameInfo.screenSize),
                GetRandomSpawnTime(gameInfo.difficulty)))
            .ToList();
    }
}
