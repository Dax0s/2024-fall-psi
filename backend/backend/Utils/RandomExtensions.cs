using backend.AimTrainerGame.Models;

namespace backend.Utils;

public static class RandomExtensions
{
    public static int NextWithinBounds(this Random random, IntBounds bounds)
        => bounds.LowerLimit > bounds.UpperLimit
            ? 0
            : random.Next(bounds.LowerLimit, bounds.UpperLimit + 1);

    public static Vector2 NextOffset(this Random random, Vector2 offsetBounds)
        => new Vector2(random.Next(offsetBounds.X), random.Next(offsetBounds.Y));

    public static DotInfo NextDotInfo(this Random random, Vector2 screenSize, IntBounds spawnTime)
        => new DotInfo(random.NextOffset(screenSize), random.NextWithinBounds(spawnTime));

}
