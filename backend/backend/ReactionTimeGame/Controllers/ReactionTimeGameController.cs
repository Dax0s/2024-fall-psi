using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Services;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.ReactionTimeGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReactionTimeGameController : ControllerBase
{
    private readonly IReactionTimeGameService _gameService;

    public ReactionTimeGameController(IReactionTimeGameService gameService)
        => _gameService = gameService;

    [HttpPost("start")]
    public ActionResult<WaitDuration> Start()
        => Ok(_gameService.NextWaitDuration());

    [HttpGet("leaderboard")]
    public ActionResult<List<ReactionTimeGameScore>> GetLeaderboard([FromQuery] ushort numberOfScores = 10)
        => Ok(_gameService.GetLeaderboard(numberOfScores));

    [HttpPost("score")]
    public ActionResult AddScore([FromBody] ScoreCreationInfo newScoreCreationInfo)
    {
        _gameService.AddScore(new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = newScoreCreationInfo.Username,
            Value = newScoreCreationInfo.Value,
            Date = DateTime.UtcNow,
        });
        return Ok();
    }
}
