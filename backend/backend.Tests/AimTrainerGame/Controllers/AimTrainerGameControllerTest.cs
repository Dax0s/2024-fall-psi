using System;
using System.Linq;
using backend.AimTrainerGame.Controllers;
using backend.AimTrainerGame.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace backend.Tests.AimTrainerGame.Controllers;

[TestSubject(typeof(AimTrainerGameController))]
public class AimTrainerGameControllerTest
{

    [Fact]
    public void StartGameShouldReturnNotNull()
    {
        ILogger<AimTrainerGameController> logger = new Logger<AimTrainerGameController>(new LoggerFactory());
        var controller = new AimTrainerGameController();

        var result = controller.StartGame(new GameStartRequest(Difficulty.EASY, new Vector2(1920, 1080)));
        Assert.True(result != null);
    }

    [Fact]
    public void StartGameEasyShouldReturn10Dots()
    {
        ILogger<AimTrainerGameController> logger = new Logger<AimTrainerGameController>(new LoggerFactory());
        var controller = new AimTrainerGameController();

        var result = controller.StartGame(new GameStartRequest(Difficulty.EASY, new Vector2(1920, 1080)));
        Assert.True(((result.Result as OkObjectResult).Value as GameStartResponse).dotInfos.Count == 10);
    }


    [Fact]
    public void StartGameHardShouldNotReturn10Dots()
    {
        ILogger<AimTrainerGameController> logger = new Logger<AimTrainerGameController>(new LoggerFactory());
        var controller = new AimTrainerGameController();

        var result = controller.StartGame(new GameStartRequest(Difficulty.HARD, new Vector2(1920, 1080)));
        Assert.True(((result.Result as OkObjectResult).Value as GameStartResponse).dotInfos.Count != 10);
    }
}
