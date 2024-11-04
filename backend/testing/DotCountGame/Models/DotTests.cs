using backend.DotCountGame.Models;
using backend.Utils;
using Xunit;

namespace testing.DotCountGame.Models;

public class DotTests
{
    [Fact]
    public void Construction()
    {
        (int x, int y, int radius) = (10, 20, 30);
        var center = new Vector2(x, y);

        var dot1 = new Dot(center, radius);
        Assert.Equal(dot1.Center, center);
        Assert.Equal(dot1.Radius, radius);

        var dot2 = new Dot(x, y, radius);
        Assert.Equal(dot2.Center, center);
        Assert.Equal(dot2.Radius, radius);
    }
}
