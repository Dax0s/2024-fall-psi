using backend.AimTrainerGame.Models;
using backend.Utils;

namespace backend.AimTrainerGame.Services;

public class AimTrainerGameService : IAimTrainerGameService
{
    private readonly IConfiguration _configuration;

    public AimTrainerGameService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private int GetRandomSpawnTime(Difficulty difficulty, Random random)
    {
        return difficulty switch
        {
            Difficulty.EASY => random.Next(
                ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:SpawnTime:easy:min", Constants.DefaultSpawnTimeMin),
                ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:SpawnTime:easy:max", Constants.DefaultSpawnTimeMax)),
            Difficulty.MEDIUM => random.Next(
                ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:SpawnTime:medium:min", Constants.DefaultSpawnTimeMin),
                ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:SpawnTime:medium:max", Constants.DefaultSpawnTimeMax)),
            Difficulty.HARD => random.Next(
                ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:SpawnTime:hard:min", Constants.DefaultSpawnTimeMin),
                ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:SpawnTime:hard:max", Constants.DefaultSpawnTimeMax)),
            _ => random.Next(Constants.DefaultSpawnTimeMin, Constants.DefaultSpawnTimeMax)
        };
    }

    public List<DotInfo> StartGame(GameStartRequest gameInfo, out int amountOfDots, out int timeToLive)
    {
        amountOfDots = gameInfo.difficulty switch
        {
            Difficulty.EASY => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:AmountOfDots:easy", Constants.DefaultDots),
            Difficulty.MEDIUM => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:AmountOfDots:medium", Constants.DefaultDots),
            Difficulty.HARD => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:AmountOfDots:hard", Constants.DefaultDots),
            _ => Constants.DefaultDots
        };

        timeToLive = gameInfo.difficulty switch
        {
            Difficulty.EASY => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:TimeToLive:easy", Constants.DefaultTimeToLive),
            Difficulty.MEDIUM => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:TimeToLive:medium", Constants.DefaultTimeToLive),
            Difficulty.HARD => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:TimeToLive:hard", Constants.DefaultTimeToLive),
            _ => Constants.DefaultTimeToLive
        };

        var random = new Random();
        return Enumerable.Range(0, amountOfDots)
            .Select(_ => new DotInfo(
                new Vector2(random.Next(gameInfo.screenSize.X), random.Next(gameInfo.screenSize.Y)),
                GetRandomSpawnTime(gameInfo.difficulty, random)))
            .ToList();
    }
}
