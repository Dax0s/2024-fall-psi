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
        for (var i = 0; i < 10; i++)
        {
            Vector2 tmp = new Vector2(random.Next(gameInfo.screenSize.X), random.Next(gameInfo.screenSize.Y));

            int spawnTime = gameInfo.difficulty switch
            {
                Difficulty.MEDIUM => random.Next(0, 1000),
                Difficulty.HARD => random.Next(0, 1000),
                _ => random.Next(0, 1000)
            };

            dots.Add(new DotInfo(tmp, spawnTime));
        }

        return Ok(dots);
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
