using backend.AimTrainerGame.Settings;
using backend.Utils;
using Xunit;

namespace testing.AimTrainerGame.Settings;

public class DifficultySettingsTests
{
    [Fact]
    public void Construction()
    {
        var (dotCount, timeToLive, spawnTime) = (10, 500, new Bounds<int>(2000, 3000));
        var difficultySettings = new DifficultySettings(dotCount, timeToLive, spawnTime);

        Assert.Equal(difficultySettings.dotCount, dotCount);
        Assert.Equal(difficultySettings.timeToLive, timeToLive);
        Assert.Equal(difficultySettings.spawnTime, spawnTime);
    }
}
