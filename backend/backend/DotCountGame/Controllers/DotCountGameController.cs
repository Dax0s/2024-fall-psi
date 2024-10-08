using backend.DotCountGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("[controller]")]
public class DotCountGameController : ControllerBase
{
    // TODO: extract constants somewhere global
    private const int MinDotCount = 1;
    private const int DotCountUpperLimit = 1000; // Inclusive

    private static bool InputIsValid(int maxDots)
    {
        return MinDotCount <= maxDots && maxDots <= DotCountUpperLimit;
    }

    [HttpGet]
    public ActionResult<DotCountCanvas> Get([FromQuery] int maxDots)
    {
        if (!InputIsValid(maxDots))
        {
            return NoContent();
        }

        return new DotCountCanvas(MinDotCount, maxDots);
    }
}
