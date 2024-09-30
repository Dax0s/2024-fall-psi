using Microsoft.AspNetCore.Mvc;
using backend.DotCountGame.Models;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("[controller]")]
public class DotCountGameController : ControllerBase
{
    [HttpGet]
    public ActionResult<DotCountCanvas> Get([FromQuery] int maxDots)
    {
        const int minDots = 1;
        if (minDots > maxDots || maxDots > 1000)
        {
            return NoContent();
        }

        return new DotCountCanvas(minDots, maxDots);
    }
}