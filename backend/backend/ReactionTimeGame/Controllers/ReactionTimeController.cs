using Microsoft.AspNetCore.Mvc;

namespace backend.ReactionTimeGame.Controllers;

using Models;

[ApiController]
[Route("[controller]")]
public class ReactionTimeController : ControllerBase
{
    [HttpGet]
    public ActionResult<ReactionTime> Get()
    {
        return new ReactionTime();
    }
}
