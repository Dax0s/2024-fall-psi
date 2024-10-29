using backend.DotCountGame.Controllers;
using backend.DotCountGame.Settings;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace testing.DotCountGame.Controllers;

public class DotCountGameControllerTests
{
    [Fact]
    public void Get()
    {
        var controller = new DotCountGameController();

        Assert.IsType<OkObjectResult>(controller.Get(10).Result);

        Assert.IsType<OkObjectResult>(controller.Get(GameSettings.DotCount.LowerLimit).Result);
        Assert.IsType<NoContentResult>(controller.Get(GameSettings.DotCount.LowerLimit - 1).Result);

        Assert.IsType<OkObjectResult>(controller.Get(GameSettings.DotCount.UpperLimit).Result);
        Assert.IsType<NoContentResult>(controller.Get(GameSettings.DotCount.UpperLimit + 1).Result);
    }
}
