using System;
using backend.Utils;

using Xunit;

namespace testing.Utils;

public class RandomVec2Tests
{
    [Theory]
    [InlineData(1600, 900, 10)]
    public void NextOffset(uint maxOffsetX, uint maxOffsetY, uint checkCount)
    {
        var random = new Random();
        var maxOffset = new Vec2<uint>(maxOffsetX, maxOffsetY);
        var xBounds = new Bounds<uint>(0, maxOffsetX);
        var yBounds = new Bounds<uint>(0, maxOffsetY);

        for (int checkIndex = 0; checkIndex < checkCount; ++checkIndex)
        {
            var randomOffset = random.NextOffset(maxOffset);
            Assert.True(xBounds.WithinBounds(randomOffset.X));
            Assert.True(yBounds.WithinBounds(randomOffset.Y));
        }
    }

}
