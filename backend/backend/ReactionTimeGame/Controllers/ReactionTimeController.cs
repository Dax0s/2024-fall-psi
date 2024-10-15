using Microsoft.AspNetCore.Mvc;

namespace backend.ReactionTimeGame.Controllers;

using Models;

[ApiController]
[Route("api/[controller]")]
public class ReactionTimeController : ControllerBase
{
    [HttpGet]
    public ActionResult<ReactionGameWaitDuration> Get()
    {
        return new ReactionGameWaitDuration();
    }
}
