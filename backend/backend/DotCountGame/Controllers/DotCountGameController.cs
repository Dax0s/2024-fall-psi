using backend.DotCountGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("[controller]")]
public class DotCountGameController : ControllerBase
{
    // TODO: extract constants somewhere global
    private const int MIN_DOT_COUNT = 1;
    private const int DOT_COUNT_UPPER_LIMIT = 1000; // Inclusive

    private static bool InputIsValid(int maxDots)
    {
        return MIN_DOT_COUNT <= maxDots && maxDots <= DOT_COUNT_UPPER_LIMIT;
    }

    [HttpGet]
    public ActionResult<DotCountCanvas> Get([FromQuery] int maxDots)
    {
        if (!InputIsValid(maxDots))
        {
            return NoContent();
        }

        return new DotCountCanvas(MIN_DOT_COUNT, maxDots);
    }
}
