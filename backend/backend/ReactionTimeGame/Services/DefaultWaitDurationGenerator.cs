using backend.ReactionTimeGame.Models;
using backend.ReactionTimeGame.Settings;
using backend.Utils;

namespace backend.ReactionTimeGame.Services;

public class DefaultWaitDurationGenerator : IWaitDurationGenerator
{
    public WaitDuration NextWaitDuration()
        => new WaitDuration((new Random()).NextWithinBounds(GameSettings.WaitBounds));
}
