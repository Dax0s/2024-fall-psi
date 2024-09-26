// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using backend.AimTrainerGame.Models;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.AimTrainerGame.Controllers;

[ApiController]
[Route("[controller]")]
public class AimTrainerGameController : ControllerBase
{
    private readonly IConfiguration Configuration;

    private readonly int DEFAULT_DOTS = 15;
    private readonly int DEFAULT_TIME_TO_LIVE = 1250;
    private readonly int DEFAULT_SPAWN_TIME_MIN = 250;
    private readonly int DEFAULT_SPAWN_TIME_MAX = 1250;

    public AimTrainerGameController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpPost("StartGame")]
    public ActionResult<GameStartResponse> StartGame([FromBody] GameStartRequest gameInfo)
    {
        int amountOfDots = gameInfo.difficulty switch
        {
            Difficulty.EASY => ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:AmountOfDots:easy", DEFAULT_DOTS),
            Difficulty.MEDIUM => ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:AmountOfDots:medium", DEFAULT_DOTS),
            Difficulty.HARD => ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:AmountOfDots:hard", DEFAULT_DOTS),
            _ => DEFAULT_DOTS
        };

        int timeToLive = gameInfo.difficulty switch
        {
            Difficulty.EASY => ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:TimeToLive:easy", DEFAULT_TIME_TO_LIVE),
            Difficulty.MEDIUM => ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:TimeToLive:medium", DEFAULT_TIME_TO_LIVE),
            Difficulty.HARD => ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:TimeToLive:hard", DEFAULT_TIME_TO_LIVE),
            _ => DEFAULT_TIME_TO_LIVE
        };

        var random = new Random();
        var dots = new List<DotInfo>(amountOfDots);
        for (var i = 0; i < amountOfDots; i++)
        {
            Vector2 tmp = new Vector2(random.Next(gameInfo.screenSize.X), random.Next(gameInfo.screenSize.Y));

            int spawnTime = gameInfo.difficulty switch
            {
                Difficulty.EASY => random.Next(
                    ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:SpawnTime:easy:min", DEFAULT_SPAWN_TIME_MIN),
                    ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:SpawnTime:easy:max", DEFAULT_SPAWN_TIME_MAX)),
                Difficulty.MEDIUM => random.Next(
                    ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:SpawnTime:medium:min", DEFAULT_SPAWN_TIME_MIN),
                    ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:SpawnTime:medium:max", DEFAULT_SPAWN_TIME_MAX)),
                Difficulty.HARD => random.Next(
                    ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:SpawnTime:hard:min", DEFAULT_SPAWN_TIME_MIN),
                    ConfigValuesParser.GetConfigIntValue(Configuration, "AimTrainerGame:SpawnTime:hard:max", DEFAULT_SPAWN_TIME_MAX)),
                _ => random.Next(DEFAULT_SPAWN_TIME_MIN, DEFAULT_SPAWN_TIME_MAX)
            };

            dots.Add(new DotInfo(tmp, spawnTime));
        }

        return Ok(new GameStartResponse(dots, amountOfDots, timeToLive));
    }
}
