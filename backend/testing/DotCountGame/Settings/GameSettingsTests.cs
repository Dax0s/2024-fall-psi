using backend.DotCountGame.Settings;
using Xunit;

namespace testing.DotCountGame.Settings;

public class GameSettingsTests
{
    [Fact]
    public void ValidateSettings()
    {
        Assert.True(GameSettings.DotCount.LowerLimit < GameSettings.DotCount.UpperLimit);

        Assert.True(0 < GameSettings.SmallRadius && GameSettings.SmallRadius <= GameSettings.DefaultRadius);
        Assert.True(GameSettings.DotCountLimitForDefaultRadius > 0);

        Assert.True(0.0f < GameSettings.MinRadiusPercentage && GameSettings.MinRadiusPercentage <= 1.0f);
    }
}
