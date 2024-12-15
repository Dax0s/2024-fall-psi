using backend.AimTrainerGame.Settings;
using Xunit;

namespace testing.AimTrainerGame.Settings;

public class GameSettingsTests
{
    [Fact]
    public void ValidSettings()
    {
        var (defaultSettings, easySettings, mediumSettings, hardSettings) = (
                GameSettings.GetDifficultySettings((Difficulty)int.MaxValue),
                GameSettings.GetDifficultySettings(Difficulty.Easy),
                GameSettings.GetDifficultySettings(Difficulty.Medium),
                GameSettings.GetDifficultySettings(Difficulty.Hard)
        );

        Assert.True(0 < defaultSettings.dotCount);
        Assert.True(0 < easySettings.dotCount);
        Assert.True(easySettings.dotCount <= mediumSettings.dotCount);
        Assert.True(mediumSettings.dotCount <= hardSettings.dotCount);

        Assert.True(0 < defaultSettings.timeToLive);
        Assert.True(0 < hardSettings.timeToLive);
        Assert.True(hardSettings.timeToLive <= mediumSettings.timeToLive);
        Assert.True(mediumSettings.timeToLive <= easySettings.timeToLive);

        Assert.True(0 <= defaultSettings.spawnTime.LowerLimit && defaultSettings.spawnTime.LowerLimit <= defaultSettings.spawnTime.UpperLimit);
        Assert.True(0 <= easySettings.spawnTime.LowerLimit && easySettings.spawnTime.LowerLimit <= easySettings.spawnTime.UpperLimit);
        Assert.True(0 <= mediumSettings.spawnTime.LowerLimit && mediumSettings.spawnTime.LowerLimit <= mediumSettings.spawnTime.UpperLimit);
        Assert.True(0 <= hardSettings.spawnTime.LowerLimit && hardSettings.spawnTime.LowerLimit <= hardSettings.spawnTime.UpperLimit);
    }
}
