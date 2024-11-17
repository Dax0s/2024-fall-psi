using backend.ReactionTimeGame.Settings;
using Xunit;

namespace testing.ReactionTimeGame.Models;

public class ReactionGameWaitDurationTests
{
    [Fact]
    public void Construction()
    {
        var reactionGameWaitDuration = new ReactionGameWaitDuration();
        Assert.True(GameSettings.WaitBounds.WithinBounds(reactionGameWaitDuration.MillisecondsToWait));
    }
}
