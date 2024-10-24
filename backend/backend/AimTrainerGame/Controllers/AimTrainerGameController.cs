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
}
