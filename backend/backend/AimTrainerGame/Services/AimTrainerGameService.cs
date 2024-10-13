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
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:easy:min", Constants.DefaultSpawnTimeMin),
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:easy:max", Constants.DefaultSpawnTimeMax)),
            Difficulty.Medium => random.Next(
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:medium:min", Constants.DefaultSpawnTimeMin),
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:medium:max", Constants.DefaultSpawnTimeMax)),
            Difficulty.Hard => random.Next(
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:hard:min", Constants.DefaultSpawnTimeMin),
                ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:SpawnTime:hard:max", Constants.DefaultSpawnTimeMax)),
            _ => random.Next(Constants.DefaultSpawnTimeMin, Constants.DefaultSpawnTimeMax)
        };
    }

    public List<DotInfo> StartGame(GameStartRequest gameInfo, out int amountOfDots, out int timeToLive)
    {
        amountOfDots = gameInfo.difficulty switch
        {
            Difficulty.Easy => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:AmountOfDots:easy", Constants.DefaultDots),
            Difficulty.Medium => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:AmountOfDots:medium", Constants.DefaultDots),
            Difficulty.Hard => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:AmountOfDots:hard", Constants.DefaultDots),
            _ => Constants.DefaultDots
        };

        timeToLive = gameInfo.difficulty switch
        {
            Difficulty.Easy => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:TimeToLive:easy", Constants.DefaultTimeToLive),
            Difficulty.Medium => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:TimeToLive:medium", Constants.DefaultTimeToLive),
            Difficulty.Hard => ConfigValuesParser.GetConfigIntValue(configuration, "AimTrainerGame:TimeToLive:hard", Constants.DefaultTimeToLive),
            _ => Constants.DefaultTimeToLive
        };

        var random = new Random();
        return Enumerable.Range(0, amountOfDots)
            .Select(_ => new DotInfo(
                random.NextVector2FromScreenSize(gameInfo.screenSize),
                GetRandomSpawnTime(gameInfo.difficulty, random)))
            .ToList();
    }
}
