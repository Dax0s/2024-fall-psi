using System.Threading.Tasks;
using backend.DotCountGame.Controllers;
using backend.DotCountGame.Services;
using backend.DotCountGame.Settings;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace testing.DotCountGame.Controllers;

public class DotCountGameControllerTests
{
    private readonly Mock<IDotCountGameService> _service;
    private readonly DotCountGameController _controller;

    public DotCountGameControllerTests()
    {
        _service = new Mock<IDotCountGameService>();
        _controller = new DotCountGameController(_service.Object);
    }

    [Fact]
    public async Task GetCanvas()
    {
        Assert.IsNotType<OkObjectResult>(
            (await _controller.GetCanvas(GameSettings.DotCount.LowerLimit - 1).ConfigureAwait(false)).Result
        );
        Assert.IsType<OkObjectResult>(
            (await _controller.GetCanvas(GameSettings.DotCount.LowerLimit).ConfigureAwait(false)).Result
        );

        Assert.IsType<OkObjectResult>(
            (await _controller.GetCanvas((GameSettings.DotCount.LowerLimit + GameSettings.DotCount.UpperLimit) / 2).ConfigureAwait(false)).Result
        );

        Assert.IsType<OkObjectResult>(
            (await _controller.GetCanvas(GameSettings.DotCount.UpperLimit).ConfigureAwait(false)).Result
        );
        Assert.IsNotType<OkObjectResult>(
            (await _controller.GetCanvas(GameSettings.DotCount.UpperLimit + 1).ConfigureAwait(false)).Result
        );
    }

    [Fact]
    public async Task GetLeaderboard()
        => Assert.IsType<OkObjectResult>((await _controller.GetLeaderboard(5).ConfigureAwait(false)).Result);

    [Fact]
    public async Task AddScore()
    {
        var score = new ScoreCreationInfo
        {
            Username = "name",
            Value = 100,
        };
        Assert.IsType<OkResult>(await _controller.AddScore(score).ConfigureAwait(false));
    }

}
