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

        const int MILLISECONDS_IN_SECOND = 1000;

        const int MIN_WAIT = 2 * MILLISECONDS_IN_SECOND;
        const int MAX_WAIT = 5 * MILLISECONDS_IN_SECOND;

        return new ReactionTime { MillisecondsToWait = Random.Shared.Next(MIN_WAIT, MAX_WAIT) };
    }
}
