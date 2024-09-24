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
    public ActionResult<GameStartValuesDTO> StartGame([FromBody] GameStartPropertiesDTO gameInfo)
    {
        var random = new Random();
        List<DotInfo> dots = [];
        var spawnTime = 0;
        for (var i = 0; i < 10; i++)
        {
            var tmp = new Vector2(random.Next(gameInfo.screenSize.X), random.Next(gameInfo.screenSize.Y));

            switch (gameInfo.difficulty)
            {
                case Difficulty.HARD:
                    spawnTime += random.Next(0, 1000);
                    break;
                case Difficulty.MEDIUM:
                    spawnTime += random.Next(500, 1500);
                    break;
                default:
                    spawnTime += random.Next(1000, 2000);
                    break;
            }

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
