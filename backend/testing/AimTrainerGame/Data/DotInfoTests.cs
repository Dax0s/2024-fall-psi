using backend.AimTrainerGame.Data;
using backend.Utils;
using Xunit;

namespace testing.AimTrainerGame.Data;

public class DotInfoTests
{
    [Fact]
    public void Construction()
    {
        var (position, spawnTime) = (new Vec2<int>(69, 420), 1792);
        var dotInfo = new DotInfo(position, spawnTime);

        Assert.Equal(position, dotInfo.Pos);
        Assert.Equal(spawnTime, dotInfo.SpawnTime);
    }
}
