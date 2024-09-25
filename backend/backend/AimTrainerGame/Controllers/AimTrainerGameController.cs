// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using backend.AimTrainerGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.AimTrainerGame.Controllers;

[ApiController]
[Route("[controller]")]
public class AimTrainerGameController : ControllerBase
{
    [HttpPost("StartGame")]
    public ActionResult<GameStartResponse> StartGame([FromBody] GameStartRequest gameInfo)
    {
        var random = new Random();
        List<DotInfo> dots = [];

        int amountOfDots = gameInfo.difficulty switch
        {
            Difficulty.MEDIUM => 15,
            Difficulty.HARD => 20,
            _ => 10
        };

        int timeToLive = gameInfo.difficulty switch
        {
            Difficulty.MEDIUM => 1250,
            Difficulty.HARD => 1000,
            _ => 1500
        };

        for (var i = 0; i < amountOfDots; i++)
        {
            Vector2 tmp = new Vector2(random.Next(gameInfo.screenSize.X), random.Next(gameInfo.screenSize.Y));

            int spawnTime = gameInfo.difficulty switch
            {
                Difficulty.MEDIUM => random.Next(250, 1250),
                Difficulty.HARD => random.Next(0, 1000),
                _ => random.Next(500, 1500)
            };

            dots.Add(new DotInfo(tmp, spawnTime));
        }

        return Ok(new GameStartResponse(dots, amountOfDots, timeToLive));
    }

    // [HttpGet("ScreenSize")]
    // public ActionResult<Vector2> GetScreenSize()
    // {
        // Console.WriteLine($"Screen size: {_screenSize.X} {_screenSize.Y}");
        // return Ok(_screenSize);
    // }

    // [HttpGet]
    // public ActionResult<string> Get()
    // {
        // {
            // when: time,
            // where: Pos
        // }
        // return Ok("Test string");
    // }
}
