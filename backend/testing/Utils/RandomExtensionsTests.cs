using System;
using backend.Utils;

using Xunit;

namespace testing.Utils;

public class RandomExtensionsTests
{
    [Theory]
    [InlineData(-100, 100, 10)]
    public void NextWithinIntBounds(int lowerBound, int upperBound, int checkCount)
    {
        var random = new Random();
        var bounds = new Bounds<int>(lowerBound, upperBound);

        for (int checkIndex = 0; checkIndex < checkCount; ++checkIndex)
        {
            var randomInt = random.NextWithinBounds(bounds);
            Assert.True(bounds.WithinBounds(randomInt));
        }
    }

    [Theory]
    [InlineData(1600, 900, 10)]
    public void NextOffset(int maxOffsetX, int maxOffsetY, int checkCount)
    {
        var random = new Random();
        var maxOffset = new Vec2<int>(maxOffsetX, maxOffsetY);
        var xBounds = new Bounds<int>(0, maxOffsetX);
        var yBounds = new Bounds<int>(0, maxOffsetY);

        for (int checkIndex = 0; checkIndex < checkCount; ++checkIndex)
        {
            var randomOffset = random.NextOffset(maxOffset);
            Assert.True(xBounds.WithinBounds(randomOffset.X));
            Assert.True(yBounds.WithinBounds(randomOffset.Y));
        }
    }

}
