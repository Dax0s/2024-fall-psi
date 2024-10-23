
using backend.Utils;
using Xunit;

namespace testing.Utils;

public class NumbersTests
{
    [Theory]

    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 4)]
    [InlineData(3, 4)]

    [InlineData((1000 * 1000) - 2, (1000 * 1000))]
    [InlineData((1000 * 1000) - 1, (1000 * 1000))]
    [InlineData((1000 * 1000) + 0, (1000 * 1000))]
    [InlineData((1000 * 1000) + 1, (1001 * 1001))]
    [InlineData((1000 * 1000) + 2, (1001 * 1001))]

    [InlineData(-1, 0)]
    [InlineData(-1_000, 0)]
    [InlineData(-1_000_000, 0)]

    public void PerfectSquare(int number, int nextPerfectSquare)
    {
        var (n, nSquared) = Numbers.NextPerfectSquare(number);

        Assert.True(number <= nSquared);
        Assert.Equal(n * n, nSquared);
        Assert.Equal(nSquared, nextPerfectSquare);
    }

}
