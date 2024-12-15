
using backend.Utils;
using Xunit;

namespace testing.Utils;

public class Vec2Tests
{
    [Fact]
    public void ConstructionEmpty()
    {
        var (vecInt, vecFloat) = (new Vec2<int>(), new Vec2<float>());

        Assert.Equal((vecInt.X, vecInt.Y), (0, 0));
        Assert.Equal((vecFloat.X, vecFloat.Y), (0.0f, 0.0f));
    }

    [Fact]
    public void ConstructionCopy()
    {
        var vec = new Vec2<int>(1, 2);
        var vecCopy = new Vec2<int>(vec);

        Assert.Equal(vec, vecCopy);
    }

    [Fact]
    public void ConstructionFromCoords()
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

    [Fact]
    public void ScalarMultiplication()
    {
        Assert.Equal(1 * new Vec2<int>(0, 0), new Vec2<int>(0, 0));
        Assert.Equal(2 * new Vec2<int>(2, 3), new Vec2<int>(4, 6));
        Assert.Equal(3 * new Vec2<int>(-4, -1), new Vec2<int>(-12, -3));
        Assert.Equal(4 * new Vec2<int>(-1, 20), new Vec2<int>(-4, 80));
    }
}
