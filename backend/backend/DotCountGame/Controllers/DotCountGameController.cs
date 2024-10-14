using backend.DotCountGame.Models;
using backend.Properties;
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
        if (!Settings.DotCountGame.DotCount.WithinBounds(maxDots))
        {
            return NoContent();
        }

        var dotCountBounds = new IntBounds(Settings.DotCountGame.DotCount.LowerLimit, maxDots);
        return new DotCountCanvas(dotCountBounds);
    }
}
