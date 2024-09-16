// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Linq;
using backend.Controllers;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Xunit;

namespace backend.Tests.Controllers;

[TestSubject(typeof(WeatherForecastController))]
public class WeatherForecastControllerTest
{
    [Fact]
    public void GetWeatherForecastShouldReturnData()
    {
        ILogger<WeatherForecastController> logger = new Logger<WeatherForecastController>(new LoggerFactory());
        var controller = new WeatherForecastController(logger);

        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = controller.Get().Count();
        Assert.True(result > 0);
    }
}
