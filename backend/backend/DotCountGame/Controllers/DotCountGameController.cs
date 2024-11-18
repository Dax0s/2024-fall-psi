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
    public ActionResult<DotCountCanvas> GetCanvas([FromBody] int maxDotCount)
    {
        if (!GameSettings.DotCount.WithinBounds(maxDotCount))
        {
            return BadRequest();
        }

        return Ok(_gameService.GenerateNextCanvas(new DefaultDotCanvasGenerator(), maxDotCount));
    }

    [HttpGet("leaderboard")]
    public ActionResult<List<DotCountGameScore>> GetLeaderboard([FromQuery] ushort numberOfScores = 10)
        => Ok(_gameService.GetLeaderboard(numberOfScores));

    [HttpPost("score")]
    public ActionResult AddScore([FromBody] ScoreCreationInfo newScoreCreationInfo)
    {
        _gameService.AddScore(new DotCountGameScore
        {
            Id = Guid.NewGuid(),
            Username = newScoreCreationInfo.Username,
            Value = newScoreCreationInfo.Value,
            Date = DateTime.UtcNow,
        });
        return Ok();
    }
}
