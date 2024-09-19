// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

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
        Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:3000");

        const int millisecondsInSecond = 1000;

        const int minWait = 2 * millisecondsInSecond;
        const int maxWait = 5 * millisecondsInSecond;

        return new ReactionTime { MillisecondsToWait = Random.Shared.Next(minWait, maxWait) };
    }
}
