using backend.AimTrainerGame.Data;
using backend.AimTrainerGame.Models;
using backend.AimTrainerGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.AimTrainerGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AimTrainerGameController : ControllerBase
{
    private readonly IAimTrainerGameService _service;

    public AimTrainerGameController(IAimTrainerGameService service)
    {
        _service = service;
    }

    [HttpPost("StartGame")]
    public async Task<ActionResult<GameStartResponse>> StartGame([FromBody] GameStartRequest gameInfo)
    {
        if ((int)gameInfo.difficulty is < 0 or > 2)
        {
            return BadRequest();
        }

        var (dots, difficultySettings) = await _service.StartGame(gameInfo).ConfigureAwait(false);

        return Ok(new GameStartResponse(dots, difficultySettings.dotCount, difficultySettings.timeToLive));
    }

    [HttpGet("Highscores")]
    public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscores([FromQuery] int amount = 10)
    {
        if (amount < 1)
        {
            return BadRequest("Amount must be greater than 0");
        }

        amount = Math.Min(amount, 100);

        var highscores = await _service.GetHighscores(amount).ConfigureAwait(false);

        return Ok(highscores);
    }

    [HttpPost("Highscores")]
    public async Task<ActionResult<Highscore>> EndGame([FromBody] GameEndRequest gameInfo)
    {
        if (string.IsNullOrWhiteSpace(gameInfo.Username))
        {
            return BadRequest();
        }

        if (gameInfo.Score <= 0)
        {
            return BadRequest();
        }

        var highscore = await _service.EndGame(gameInfo).ConfigureAwait(false);

        return Ok(highscore);
    }

}
