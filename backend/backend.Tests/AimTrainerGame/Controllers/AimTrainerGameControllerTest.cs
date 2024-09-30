using backend.AimTrainerGame.Controllers;
using backend.AimTrainerGame.Models;
using backend.Utils;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace backend.Tests.AimTrainerGame.Controllers;

[TestSubject(typeof(AimTrainerGameController))]
public class AimTrainerGameControllerTest
{

    [Fact]
    public void StartGameShouldReturnNotNull()
    {
        IConfiguration config = new ConfigurationBuilder().Build();
        var controller = new AimTrainerGameController(config);

        var result = controller.StartGame(new GameStartRequest(Difficulty.EASY, new Vector2(1920, 1080)));
        Assert.True(result != null);
    }

    [Fact]
    public void StartGameHardShouldNotReturn10Dots()
    {
        IConfiguration config = new ConfigurationBuilder().Build();
        var controller = new AimTrainerGameController(config);

        var result = controller.StartGame(new GameStartRequest(Difficulty.HARD, new Vector2(1920, 1080)));
        Assert.True(((result.Result as OkObjectResult).Value as GameStartResponse).dotInfos.Count != 10);
    }
}
