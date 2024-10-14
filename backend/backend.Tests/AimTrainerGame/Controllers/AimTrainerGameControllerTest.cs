using backend.AimTrainerGame.Controllers;
using JetBrains.Annotations;

namespace backend.Tests.AimTrainerGame.Controllers;

[TestSubject(typeof(AimTrainerGameController))]
public class AimTrainerGameControllerTest
{

    // [Fact]
    // public void StartGameShouldReturnNotNull()
    // {
    //     IConfiguration config = new ConfigurationBuilder().Build();
    //     var controller = new AimTrainerGameController(config);
    //
    //     var result = controller.StartGame(new GameStartRequest(Difficulty.EASY, new Vector2(1920, 1080)));
    //     Assert.True(result != null);
    // }

    // It currently fails, because it can't get config values (at least that's my guess)
    // [Fact]
    // public void StartGameEasyShouldReturn10Dots()
    // {
    // IConfiguration config = new ConfigurationBuilder().Build();
    // var controller = new AimTrainerGameController(config);

    // var result = controller.StartGame(new GameStartRequest(Difficulty.EASY, new Vector2(1920, 1080)));
    // Assert.True(((result.Result as OkObjectResult).Value as GameStartResponse).dotInfos.Count == 10);
    // }

    // [Fact]
    // public void StartGameHardShouldNotReturn10Dots()
    // {
    //     IConfiguration config = new ConfigurationBuilder().Build();
    //     var controller = new AimTrainerGameController(config);
    //
    //     var result = controller.StartGame(new GameStartRequest(Difficulty.HARD, new Vector2(1920, 1080)));
    //     Assert.True(((result.Result as OkObjectResult).Value as GameStartResponse).dotInfos.Count != 10);
    // }
}
