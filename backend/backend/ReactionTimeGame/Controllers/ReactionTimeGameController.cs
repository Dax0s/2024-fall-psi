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
    public async Task<ActionResult<List<ReactionTimeGameScore>>> GetLeaderboard([FromQuery] ushort numberOfScores = 10)
        => Ok(await _gameService.GetLeaderboardAsync(numberOfScores).ConfigureAwait(false));

    [HttpPost("score")]
    public async Task<ActionResult> AddScore([FromBody] ScoreCreationInfo newScoreCreationInfo)
    {
        await _gameService.AddScoreAsync(new ReactionTimeGameScore
        {
            Id = Guid.NewGuid(),
            Username = newScoreCreationInfo.Username,
            Value = newScoreCreationInfo.Value,
            Date = DateTime.UtcNow,
        }).ConfigureAwait(false); ;
        return Ok();
    }
}
