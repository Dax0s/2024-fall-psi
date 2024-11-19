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
    public ActionResult<GameStartResponse> StartGame([FromBody] GameStartRequest gameInfo)
    {
        if ((int)gameInfo.difficulty is < 0 or > 2)
        {
            return BadRequest();
        }

        var (dots, difficultySettings) = _service.StartGame(gameInfo);

        return Ok(new GameStartResponse(dots, difficultySettings.dotCount, difficultySettings.timeToLive));
    }

    [HttpGet("Highscores")]
    public ActionResult<IEnumerable<Highscore>> GetHighscores([FromQuery] int amount = 10)
    {
        if (amount < 1)
        {
            return BadRequest("Amount must be greater than 0");
        }

        amount = Math.Min(amount, 100);

        return Ok(_service.GetHighscores(amount));
    }

    [HttpPost("Highscores")]
    public ActionResult<Highscore> EndGame([FromBody] GameEndRequest gameInfo)
    {
        if (gameInfo.Username.Trim().Length == 0)
        {
            return BadRequest();
        }

        if (gameInfo.Score <= 0)
        {
            return BadRequest();
        }

        return Ok(_service.EndGame(gameInfo));
    }
}
