using System;
using backend.Utils;
using Xunit;

namespace testing.Utils;

public class RandomBoundsTests
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

}
