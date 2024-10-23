using backend.Utils;
using Xunit;

namespace testing.Utils;

public class Vector2Tests
{
    [Fact]
    public void Construction()
    {
        var (someX, someY) = (100, -50);

        var someVector = new Vector2(someX, someY);
        Assert.Equal((someX, someY), (someVector.X, someVector.Y));

        Assert.Equal(new Vector2(someX, someY), new Vector2(x: someX, y: someY));
    }

    [Fact]
    public void Addition()
    {
        Assert.Equal(new Vector2(0, 0) + new Vector2(0, 0), new Vector2(0, 0));
        Assert.Equal(new Vector2(0, 1) + new Vector2(2, 3), new Vector2(2, 4));
        Assert.Equal(new Vector2(-3, -1) + new Vector2(-4, -1), new Vector2(-7, -2));
        Assert.Equal(new Vector2(10, -15) + new Vector2(-1, 20), new Vector2(9, 5));
    }

    [Fact]
    public void Subtraction()
    {
        Assert.Equal(new Vector2(0, 0) - new Vector2(0, 0), new Vector2(0, 0));
        Assert.Equal(new Vector2(0, 1) - new Vector2(2, 3), new Vector2(-2, -2));
        Assert.Equal(new Vector2(-3, -1) - new Vector2(-4, -1), new Vector2(1, 0));
        Assert.Equal(new Vector2(10, -15) - new Vector2(-1, 20), new Vector2(11, -35));
    }
}
