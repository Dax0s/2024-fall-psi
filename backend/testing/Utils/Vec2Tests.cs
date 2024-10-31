
using backend.Utils;
using Xunit;

namespace testing.Utils;

public class Vec2Tests
{
    [Fact]
    public void Construction()
    {
        var (someX, someY) = (100, -50);

        var someVector = new Vec2<int>(someX, someY);
        Assert.Equal((someX, someY), (someVector.X, someVector.Y));

        Assert.Equal(new Vec2<int>(someX, someY), new Vec2<int>(x: someX, y: someY));
    }

    [Fact]
    public void Addition()
    {
        Assert.Equal(new Vec2<int>(0, 0) + new Vec2<int>(0, 0), new Vec2<int>(0, 0));
        Assert.Equal(new Vec2<int>(0, 1) + new Vec2<int>(2, 3), new Vec2<int>(2, 4));
        Assert.Equal(new Vec2<int>(-3, -1) + new Vec2<int>(-4, -1), new Vec2<int>(-7, -2));
        Assert.Equal(new Vec2<int>(10, -15) + new Vec2<int>(-1, 20), new Vec2<int>(9, 5));
    }

    [Fact]
    public void Subtraction()
    {
        Assert.Equal(new Vec2<int>(0, 0) - new Vec2<int>(0, 0), new Vec2<int>(0, 0));
        Assert.Equal(new Vec2<int>(0, 1) - new Vec2<int>(2, 3), new Vec2<int>(-2, -2));
        Assert.Equal(new Vec2<int>(-3, -1) - new Vec2<int>(-4, -1), new Vec2<int>(1, 0));
        Assert.Equal(new Vec2<int>(10, -15) - new Vec2<int>(-1, 20), new Vec2<int>(11, -35));
    }
}
