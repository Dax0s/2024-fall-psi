using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.AimTrainerGame.Controllers;
using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Services;
using backend.AimTrainerGame.Settings;
using backend.Utils;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Range = Moq.Range;

namespace testing.AimTrainerGame.Controllers;

[TestSubject(typeof(AimTrainerGameController))]
public class AimTrainerGameControllerTest
{
    private const int MaxHighscoresReturned = 100;

    private readonly Mock<IAimTrainerGameService> _service;
    private readonly AimTrainerGameController _controller;

    private readonly DifficultySettings _mockDifficultySettings = new(0, 0, new Bounds<int>(0, 10));

    public AimTrainerGameControllerTest()
    {
        _service = new Mock<IAimTrainerGameService>();
        _controller = new AimTrainerGameController(_service.Object);
    }

    #region StartGame Tests

    [Fact]
    public async Task StartGame_ValidRequest_ReturnsOkResult()
    {
        var request = new GameStartRequest(Difficulty.Easy, new Vec2<int>(1920, 1080));
        _service
            .Setup(s => s.StartGame(It.IsAny<GameStartRequest>()))
            .ReturnsAsync((new List<DotInfo>(), _mockDifficultySettings));

        var result = await _controller.StartGame(request).ConfigureAwait(true);

        Assert.IsType<ActionResult<GameStartResponse>>(result);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(4)]
    public async Task StartGame_InvalidDifficulty_ReturnsBadRequest(int difficulty)
    {
        var request = new GameStartRequest((Difficulty)difficulty, new Vec2<int>(1920, 1080));

        var result = await _controller.StartGame(request).ConfigureAwait(true);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    #endregion

    #region GetHighScores Tests

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public async Task GetHighscores_ValidRequest_ReturnsOkResult(int amount)
    {
        var highscores = new List<Highscore>(amount);
        for (var i = 0; i < amount; i++)
        {
            highscores.Add(new Highscore());
        }

        _service.Setup(s => s.GetHighscores(amount)).ReturnsAsync(highscores);

        var result = await _controller.GetHighscores(amount).ConfigureAwait(true);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var resultValue = Assert.IsAssignableFrom<IEnumerable<Highscore>>(okResult.Value);
        Assert.Equal(highscores, resultValue);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetHighscores_InvalidRequest_ReturnsBadResult(int amount)
    {
        var result = await _controller.GetHighscores(amount).ConfigureAwait(true);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(2200)]
    public async Task GetHighscores_ValidRequest_ReturnsMax100Highscores(int amount)
    {
        var expectedHighscores = new List<Highscore>(Math.Min(amount, MaxHighscoresReturned));

        _service
            .Setup(s => s.GetHighscores(It.IsInRange(1, 100, Range.Inclusive)))
            .ReturnsAsync(expectedHighscores);

        var result = await _controller.GetHighscores(amount).ConfigureAwait(true);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var resultHighscores = Assert.IsAssignableFrom<IEnumerable<Highscore>>(okResult.Value);
        Assert.Equal(expectedHighscores, resultHighscores);
    }

    #endregion

    #region EndGame Tests

    [Theory]
    [InlineData("Username", 4)]
    [InlineData("Domas", 10)]
    [InlineData("Zaidejas", 20)]
    public async Task EndGame_ValidRequest_ReturnsOkResult(string username, int score)
    {
        _service
            .Setup(s => s.EndGame(It.IsAny<GameEndRequest>()))
            .ReturnsAsync(new Highscore());
        var result = await _controller.EndGame(new GameEndRequest(username, score)).ConfigureAwait(true);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task EndGame_EmptyUsername_ReturnsBadRequest()
    {
        var result = await _controller.EndGame(new GameEndRequest("", 10)).ConfigureAwait(true);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public async Task EndGame_ScoreLowerThan1_ReturnsBadRequest(int score)
    {
        var result = await _controller.EndGame(new GameEndRequest("Username", score)).ConfigureAwait(true);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    #endregion
}
