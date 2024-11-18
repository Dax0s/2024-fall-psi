using backend.Utils;
using Xunit;

namespace testing.Tests.Utils;

public class BoundsTests
{
    [Fact]
    public void Construction()
    {
        var (lowerLimit, upperLimit) = (10, 20);
        var bounds = new Bounds<int>(lowerLimit, upperLimit);
        Assert.Equal((lowerLimit, upperLimit), (bounds.LowerLimit, bounds.UpperLimit));
    }

    [Theory]
    [InlineData(0, 0, new int[] { 0 }, new int[] { -1, 1 })]
    [InlineData(10, 15, new int[] { 10, 11, 12, 13, 14, 15 }, new int[] { 8, 9, 16, 17 })]
    [InlineData(-15, -10, new int[] { -15, -14, -13, -12, -11, -10 }, new int[] { -17, -16, -9, -8 })]
    [InlineData(-2, 2, new int[] { -2, -1, 0, 1, 2 }, new int[] { -3, 3 })]
    [InlineData(int.MinValue, int.MinValue, new int[] { int.MinValue }, new int[] { int.MinValue + 1 })]
    [InlineData(int.MaxValue, int.MaxValue, new int[] { int.MaxValue }, new int[] { int.MaxValue - 1 })]
    [InlineData(1, -1, new int[] { -1, 0, 1 }, new int[] { -2, 2 })]
    public void WithinBounds(int lowerLimit, int upperLimit, int[] inRangeValues, int[] outOfRangleValues)
    {
        var bounds = new Bounds<int>(lowerLimit, upperLimit);

        foreach (var inRangeValue in inRangeValues)
        {
            Assert.True(bounds.WithinBounds(inRangeValue));
        }

        foreach (var outOfRangeValue in outOfRangleValues)
        {
            Assert.False(bounds.WithinBounds(outOfRangeValue));
        }
    }

}
