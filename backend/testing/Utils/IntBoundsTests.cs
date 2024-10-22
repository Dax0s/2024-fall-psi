using backend.Utils;
using Xunit;

namespace testing.Tests.Utils;

public class IntBoundsTests
{
    [Fact]
    public void Construction()
    {
        var (lowerLimit, upperLimit) = (10, 20);
        var bounds = new IntBounds(lowerLimit, upperLimit);
        Assert.Equal((lowerLimit, upperLimit), (bounds.LowerLimit, bounds.UpperLimit));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(10, 15)]
    [InlineData(-15, -10)]
    [InlineData(-10, 10)]
    [InlineData(int.MinValue, int.MinValue)]
    [InlineData(int.MaxValue, int.MaxValue)]
    [InlineData(1, 0)]
    public void WithinBounds(int lowerLimit, int upperLimit)
    {
        var bounds = new IntBounds(lowerLimit, upperLimit);

        for (int valueWithin = lowerLimit; valueWithin <= upperLimit; ++valueWithin)
        {
            Assert.True(bounds.WithinBounds(valueWithin));

            if (valueWithin == int.MaxValue)
            {
                break;
            }
        }

        if (int.MaxValue < lowerLimit)
        {
            Assert.False(bounds.WithinBounds(lowerLimit - 1));
        }
        if (upperLimit < int.MaxValue)
        {
            Assert.False(bounds.WithinBounds(upperLimit + 1));
        }
    }

}
