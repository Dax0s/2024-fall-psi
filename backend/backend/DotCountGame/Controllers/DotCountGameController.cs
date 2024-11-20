using backend.DotCountGame.Data;
using backend.DotCountGame.Logic;
using backend.DotCountGame.Models;
using backend.DotCountGame.Services;
using backend.DotCountGame.Settings;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DotCountGameController : ControllerBase
{
    private readonly IDotCountGameService _gameService;

    public DotCountGameController(IDotCountGameService gameService)
        => _gameService = gameService;

    [HttpPost("getcanvas")]
    public async Task<ActionResult<DotCountCanvas>> GetCanvasAsync([FromBody] int maxDotCount)
    {
        if (!GameSettings.DotCount.WithinBounds(maxDotCount))
        {
            return BadRequest();
        }

        var canvas = await _gameService
            .GenerateNextCanvasAsync(new DefaultDotCanvasGenerator(), maxDotCount)
            .ConfigureAwait(false);

        return Ok(canvas);
    }

    [HttpGet("leaderboard")]
    public async Task<ActionResult<List<DotCountGameScore>>> GetLeaderboardAsync([FromQuery] ushort numberOfScores = 10)
    {
        var leaderboard = await _gameService
            .GetLeaderboardAsync(numberOfScores)
            .ConfigureAwait(false);

        return Ok(leaderboard);
    }

    [HttpPost("score")]
    public async Task<ActionResult> AddScoreAsync([FromBody] ScoreCreationInfo newScoreCreationInfo)
    {
        var newScore = new DotCountGameScore
        {
            Id = Guid.NewGuid(),
            Username = newScoreCreationInfo.Username,
            Value = newScoreCreationInfo.Value,
            Date = DateTime.UtcNow,
        };

        await _gameService.AddScoreAsync(newScore).ConfigureAwait(false);

        return Ok();
    }
}
