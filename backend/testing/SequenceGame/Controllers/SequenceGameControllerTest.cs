using System.Collections.Generic;
using backend.SequenceGame.Controllers;
using backend.SequenceGame.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace testing.SequenceGame.Controllers;

[TestSubject(typeof(SequenceGameController))]
public class SequenceGameControllerTest
{
    private readonly Mock<ISequenceGameService> _service;
    private readonly SequenceGameController _controller;

    public SequenceGameControllerTest()
    {
        _service = new Mock<ISequenceGameService>();
        _controller = new SequenceGameController(_service.Object);
    }

    [Fact]
    public void GetSequence_ValidSequence_ReturnsOkResult()
    {
        const string validSequence = "1,2,3";
        _service.Setup(s => s.GetSequence(validSequence)).Returns([1, 2, 3, 4]);

        var result = _controller.GetSequence(validSequence);

        Assert.IsType<ActionResult<List<int>>>(result);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetSequence_EmptySequence_ReturnsOkResult()
    {
        _service.Setup(s => s.GetSequence("")).Returns([1]);

        var result = _controller.GetSequence("");

        Assert.IsType<ActionResult<List<int>>>(result);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Theory]
    [InlineData("1,2,0")]
    [InlineData("a,2,3")]
    [InlineData("1,2,10")]
    [InlineData("1;2")]
    public void GetSequence_InvalidSequence_ReturnsBadRequest(string sequence)
    {
        var result = _controller.GetSequence(sequence);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}
