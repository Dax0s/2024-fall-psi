using backend.AimTrainerGame.Models;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.AimTrainerGame.Controllers;

[ApiController]
[Route("[controller]")]
public class AimTrainerGameController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AimTrainerGameController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("StartGame")]
    public ActionResult<GameStartResponse> StartGame([FromBody] GameStartRequest gameInfo)
    {
        var amountOfDots = gameInfo.difficulty switch
        {
            Difficulty.EASY => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:AmountOfDots:easy", Constants.DefaultDots),
            Difficulty.MEDIUM => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:AmountOfDots:medium", Constants.DefaultDots),
            Difficulty.HARD => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:AmountOfDots:hard", Constants.DefaultDots),
            _ => Constants.DefaultDots
        };

        var timeToLive = gameInfo.difficulty switch
        {
            Difficulty.EASY => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:TimeToLive:easy", Constants.DefaultTimeToLive),
            Difficulty.MEDIUM => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:TimeToLive:medium", Constants.DefaultTimeToLive),
            Difficulty.HARD => ConfigValuesParser.GetConfigIntValue(_configuration, "AimTrainerGame:TimeToLive:hard", Constants.DefaultTimeToLive),
            _ => Constants.DefaultTimeToLive
        };

        var random = new Random();
        var dots = new List<DotInfo>(amountOfDots);
        for (var i = 0; i < amountOfDots; i++)
        {
            var tmp = new Vector2(random.Next(gameInfo.screenSize.X), random.Next(gameInfo.screenSize.Y));

            var spawnTime = gameInfo.difficulty switch
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

            dots.Add(new DotInfo(tmp, spawnTime));
        }

        return Ok(new GameStartResponse(dots, amountOfDots, timeToLive));
    }
}
