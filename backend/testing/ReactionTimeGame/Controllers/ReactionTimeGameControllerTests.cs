using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

using backend.ReactionTimeGame.Controllers;
using backend.ReactionTimeGame.Services;
using System.Threading.Tasks;
using backend.Utils;

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
    {
        Assert.True(_controller.Start().Result is OkObjectResult);
    }

    [Fact]
    public async Task GetLeaderboard()
    {
        var result = await _controller.GetLeaderboard();
        Assert.True(result.Result is OkObjectResult);
    }

    [Fact]
    public async Task AddScore()
    {
        var newScoreCreationInfo = new ScoreCreationInfo
        {
            Username = "somePrettyUsername",
            Value = 42,
        };
        var result = await _controller.AddScore(newScoreCreationInfo);

        Assert.True(result is OkResult);
    }

}
