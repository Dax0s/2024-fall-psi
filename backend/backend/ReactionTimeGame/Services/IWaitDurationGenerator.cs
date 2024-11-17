using backend.ReactionTimeGame.Models;

namespace backend.ReactionTimeGame.Services;

public interface IWaitDurationGenerator
{
    public WaitDuration NextWaitDuration();
}
