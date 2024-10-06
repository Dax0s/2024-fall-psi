using backend.DotCountGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("[controller]")]
public class DotCountGameController : ControllerBase
{
    [HttpGet]
    public ActionResult<DotCountCanvas> Get([FromQuery] int maxDots)
    {
        const int minDots = 1;
        const int maxDotsLimit = 1000;
        if (minDots > maxDots || maxDots > maxDotsLimit)
        {
            return NoContent();
        }

        return new DotCountCanvas(minDots, maxDots);
    }
}
