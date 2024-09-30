using Microsoft.AspNetCore.Mvc;

namespace backend.ReactionTimeGame.Controllers;

using Models;

[ApiController]
[Route("[controller]")]
public class ReactionTimeController : ControllerBase
{

    private readonly ILogger<ReactionTimeController> _logger;

    public ReactionTimeController(ILogger<ReactionTimeController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetNumber")]
    public ReactionTime Get()
    {

        const int millisecondsInSecond = 1000;

        const int minWait = 2 * millisecondsInSecond;
        const int maxWait = 5 * millisecondsInSecond;

        return new ReactionTime { MillisecondsToWait = Random.Shared.Next(minWait, maxWait) };
    }
}
