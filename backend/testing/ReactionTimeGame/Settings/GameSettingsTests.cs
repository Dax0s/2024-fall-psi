using backend.ReactionTimeGame.Settings;
using Xunit;

namespace testing.ReactionTimeGame.Settings;

public class GameSettingsTests
{
    [Fact]
    public void WaitBoundsTest()
    {
        Assert.True(GameSettings.WaitBounds.LowerLimit < GameSettings.WaitBounds.UpperLimit);
    }
}
