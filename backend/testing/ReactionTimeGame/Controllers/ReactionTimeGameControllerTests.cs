using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

using backend.ReactionTimeGame.Controllers;
using backend.ReactionTimeGame.Services;

namespace testing.ReactionTimeGame.Controllers;

public class ReactionTimeGameControllerTests
{
    private Mock<IReactionTimeGameService> _service;
    private Mock<ReactionTimeGameController> _controller;

    public ReactionTimeGameControllerTests()
    {
        _service = new Mock<IReactionTimeGameService>();
        _controller = new Mock<ReactionTimeGameController>(_service.Object);
    }

    [Fact]
    public void Start()
    {
        Assert.IsType<OkObjectResult>(_controller.Object.Start());
    }

}
