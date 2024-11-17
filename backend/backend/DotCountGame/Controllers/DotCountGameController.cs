using backend.DotCountGame.Data;
using backend.DotCountGame.Services;
using backend.DotCountGame.Settings;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.DotCountGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DotCountGameController : ControllerBase
{
    private readonly IDotCountGameInfoGenerator _canvasGenerator;

    public DotCountGameController(IDotCountGameInfoGenerator canvasGenerator)
    {
        _canvasGenerator = canvasGenerator;
    }

    [HttpGet]
    public ActionResult<DotCountCanvas> Get([FromQuery] int maxDots)
    {
        if (!GameSettings.DotCount.WithinBounds(maxDots))
        {
            return BadRequest();
        }

        var dotCountBounds = new Bounds<int>(GameSettings.DotCount.LowerLimit, maxDots);
        return Ok(_canvasGenerator.GenerateNextCanvas(dotCountBounds));
    }
}
