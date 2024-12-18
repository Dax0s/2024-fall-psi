using System.Collections.Generic;
using System.Linq;
using backend.MemoryGameWithNumbers.Controllers;
using backend.MemoryGameWithNumbers.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace testing.MemoryGameWithNumbers.Controllers;

public class MemoryGameWithNumbersControllerTests
{
    private const int SuccessStatusCode = 200;
    private readonly MemoryGameWithNumbersController _controller;
    private readonly MemoryGameService _memoryGameService;

    public MemoryGameWithNumbersControllerTests()
    {
        // Instantiate a new service for each test to avoid shared state.
        _memoryGameService = new MemoryGameService();
        _controller = new MemoryGameWithNumbersController(_memoryGameService);
    }

    [Fact]
    public void StartGame_ShouldReturnGridWithCorrectNumbersAndNulls()
    {
        var maxNumber = 5;
        var result = _controller.StartGame(maxNumber);

        if (result.Result is OkObjectResult okResult && okResult.Value is List<int?> gridNumbers)
        {
            Assert.Equal(SuccessStatusCode, okResult.StatusCode);
            Assert.NotNull(gridNumbers);
            Assert.Equal(16, gridNumbers.Count);
            Assert.Equal(maxNumber, gridNumbers.Count(n => n != null));
            Assert.Equal(16 - maxNumber, gridNumbers.Count(n => n == null));
        }
        else
        {
            Assert.Fail("Expected an OkObjectResult with a List<int?> as the value.");
        }
    }

    [Fact]
    public void StartGame_ShouldReturnGridWithRandomizedOrder()
    {
        var maxNumber = 5;

        var result1 = _controller.StartGame(maxNumber);
        var result2 = _controller.StartGame(maxNumber);

        if (result1.Result is OkObjectResult okResult1 && result2.Result is OkObjectResult okResult2)
        {
            var gridNumbers1 = Assert.IsType<List<int?>>(okResult1.Value);
            var gridNumbers2 = Assert.IsType<List<int?>>(okResult2.Value);

            Assert.NotEqual(gridNumbers1, gridNumbers2); // Grid should be randomized
        }
    }

    [Fact]
    public void CheckAttempt_ShouldReturnBadRequest_WhenGameNotStarted()
    {
        var userAttempt = new List<int?> { 1, 2, 3, 4, 5 };

        var result = _controller.CheckAttempt(userAttempt);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);

        // Assert the anonymous object structure
        var errorResponse = badRequestResult.Value;
        Assert.NotNull(errorResponse);
        Assert.Equal("Game not started.", errorResponse.GetType().GetProperty("error")?.GetValue(errorResponse));
    }

    [Fact]
    public void CheckAttempt_ShouldReturnTrue_WhenAttemptMatchesCorrectSequence()
    {
        var maxNumber = 5;
        _controller.StartGame(maxNumber);
        var correctAttempt = Enumerable.Range(1, maxNumber).Select(n => (int?)n).ToList();

        var result = _controller.CheckAttempt(correctAttempt);

        if (result.Result is OkObjectResult okResult)
        {
            Assert.Equal(SuccessStatusCode, okResult.StatusCode);
            Assert.True((bool)okResult.Value);
        }
    }

    [Fact]
    public void CheckAttempt_ShouldReturnFalse_WhenAttemptIsShorterThanCorrectSequence()
    {
        var maxNumber = 5;
        _controller.StartGame(maxNumber);
        var shortAttempt = new List<int?> { 1, 2, 3 };

        var result = _controller.CheckAttempt(shortAttempt);

        AssertResultIsOkAndFalse(result.Result);
    }

    [Fact]
    public void CheckAttempt_ShouldReturnFalse_WhenAttemptContainsNullValues()
    {
        var maxNumber = 5;
        _controller.StartGame(maxNumber);
        var attemptWithNulls = new List<int?> { 1, 2, null, 4, 5 };

        var result = _controller.CheckAttempt(attemptWithNulls);

        AssertResultIsOkAndFalse(result.Result);
    }

    private static void AssertResultIsOkAndFalse(IActionResult result)
    {
        if (result is OkObjectResult okResult)
        {
            Assert.Equal(SuccessStatusCode, okResult.StatusCode);
            Assert.False((bool)okResult.Value);
        }
    }
}
