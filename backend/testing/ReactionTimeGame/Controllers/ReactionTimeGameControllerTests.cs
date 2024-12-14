using System.Threading.Tasks;
using backend.ReactionTimeGame.Controllers;
using backend.ReactionTimeGame.Services;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace testing.ReactionTimeGame.Controllers;

public class ReactionTimeGameControllerTests
{
    private readonly Mock<IReactionTimeGameService> _service;
    private readonly ReactionTimeGameController _controller;

    public ReactionTimeGameControllerTests()
    {
        _service = new Mock<IReactionTimeGameService>();
        _controller = new ReactionTimeGameController(_service.Object);
    }

    [Fact]
    public void Start()
        => Assert.IsType<OkObjectResult>(_controller.Start().Result);

    [Fact]
    public async Task GetLeaderboard()
        => Assert.IsType<OkObjectResult>((await _controller.GetLeaderboard().ConfigureAwait(false)).Result);

    [Fact]
    public async Task AddScore()
    {
        var newScoreCreationInfo = new ScoreCreationInfo
        {
            Username = "somePrettyUsername",
            Value = 42,
        };
        Assert.IsType<OkResult>(await _controller.AddScore(newScoreCreationInfo).ConfigureAwait(false));
    }

}
