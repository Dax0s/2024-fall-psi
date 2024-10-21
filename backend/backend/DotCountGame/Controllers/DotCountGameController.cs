using backend.DotCountGame.Models;
using backend.DotCountGame.Settings;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DotCountGameController : ControllerBase
{
    [HttpGet]
    public ActionResult<DotCountCanvas> Get([FromQuery] int maxDots)
    {
        if (!GameSettings.DotCount.WithinBounds(maxDots))
        {
            return NoContent();
        }

        return new DotCountCanvas(new IntBounds(GameSettings.DotCount.LowerLimit, maxDots));
    }
}
