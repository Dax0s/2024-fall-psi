using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Utils;
using backend.Utils;

namespace backend.AimTrainerGame.Services;

public class AimTrainerGameService(IConfiguration configuration) : IAimTrainerGameService
{
    private int GetRandomSpawnTime(Difficulty difficulty, Random random)
    {
        return difficulty switch
        {
            Difficulty.Easy => random.Next(
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:easy:min", Constants.AimTrainerGame.Defaults.SpawnTime.Min),
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:easy:max", Constants.AimTrainerGame.Defaults.SpawnTime.Max)),
            Difficulty.Medium => random.Next(
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:medium:min", Constants.AimTrainerGame.Defaults.SpawnTime.Min),
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:medium:max", Constants.AimTrainerGame.Defaults.SpawnTime.Max)),
            Difficulty.Hard => random.Next(
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:hard:min", Constants.AimTrainerGame.Defaults.SpawnTime.Min),
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:hard:max", Constants.AimTrainerGame.Defaults.SpawnTime.Max)),
            _ => random.Next(Constants.AimTrainerGame.Defaults.SpawnTime.Min, Constants.AimTrainerGame.Defaults.SpawnTime.Max)
        };
    }

    public List<DotInfo> StartGame(GameStartRequest gameInfo, out int amountOfDots, out int timeToLive)
    {
        amountOfDots = gameInfo.difficulty switch
        {
            Difficulty.Easy => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:AmountOfDots:easy", Constants.AimTrainerGame.Defaults.Dots),
            Difficulty.Medium => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:AmountOfDots:medium", Constants.AimTrainerGame.Defaults.Dots),
            Difficulty.Hard => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:AmountOfDots:hard", Constants.AimTrainerGame.Defaults.Dots),
            _ => Constants.AimTrainerGame.Defaults.Dots
        };

        timeToLive = gameInfo.difficulty switch
        {
            Difficulty.Easy => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:TimeToLive:easy", Constants.AimTrainerGame.Defaults.TimeToLive),
            Difficulty.Medium => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:TimeToLive:medium", Constants.AimTrainerGame.Defaults.TimeToLive),
            Difficulty.Hard => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:TimeToLive:hard", Constants.AimTrainerGame.Defaults.TimeToLive),
            _ => Constants.AimTrainerGame.Defaults.TimeToLive
        };

        var random = new Random();
        return Enumerable.Range(0, amountOfDots)
            .Select(_ => new DotInfo(
                random.NextVector2FromScreenSize(gameInfo.screenSize),
                GetRandomSpawnTime(gameInfo.difficulty, random)))
            .ToList();
    }
}
