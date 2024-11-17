using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.ReactionTimeGame.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReactionTimeGameController : ControllerBase
{
    private readonly IWaitDurationGenerator _waitDurationGenerator;

    public ReactionTimeGameController(IWaitDurationGenerator waitDurationGenerator)
    {
        _waitDurationGenerator = waitDurationGenerator;
    }

    [HttpPost("start")]
    public ActionResult<WaitDuration> Start()
        => Ok(_waitDurationGenerator.NextWaitDuration());
}
