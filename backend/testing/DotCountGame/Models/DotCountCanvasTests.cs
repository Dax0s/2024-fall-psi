using backend.DotCountGame.Settings;
using backend.Utils;
using Xunit;

namespace testing.DotCountGame.Models;

public class DotCountCanvasTests
{
    [Theory]
    [InlineData(1, 10)]
    [InlineData(5, 10)]
    [InlineData(10, 10)]
    [InlineData(100, 5)]
    [InlineData(1000, 5)]
    public void Generation(int maxDotCount, int maxRadius)
    {
        var dotCanvas = new DotCountCanvas(new Bounds<int>(GameSettings.DotCount.LowerLimit, maxDotCount));

        Assert.True(dotCanvas.Dots.Count <= maxDotCount);

        var minCoordValue = maxRadius;
        var maxCoordValue = dotCanvas.SideLength - maxRadius;

        Assert.All(dotCanvas.Dots, (dot) =>
        {
            Assert.True(dot.Radius <= maxRadius);
            Assert.InRange(dot.Center.X, minCoordValue, maxCoordValue);
            Assert.InRange(dot.Center.Y, minCoordValue, maxCoordValue);
        });
    }
}
