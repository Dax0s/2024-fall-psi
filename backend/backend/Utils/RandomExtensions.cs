using backend.AimTrainerGame.Models;

namespace backend.Utils;

public static class RandomExtensions
{
    public static int NextWithinBounds(this Random random, IntBounds bounds)
        => bounds.LowerLimit > bounds.UpperLimit
            ? 0
            : random.Next(bounds.LowerLimit, bounds.UpperLimit + 1);

    public static int NextWithinIntBounds(this Random random, Bounds<int> bounds)
        => bounds.LowerLimit > bounds.UpperLimit
            ? 0
            : random.Next(bounds.LowerLimit, bounds.UpperLimit + 1);

    public static Vector2 NextOffset(this Random random, Vector2 maxOffset)
        => maxOffset.X < 0 || maxOffset.Y < 0
            ? new Vector2(0, 0)
            : new Vector2(random.Next(maxOffset.X), random.Next(maxOffset.Y));

    public static DotInfo NextDotInfo(this Random random, Vector2 screenSize, Bounds<int> spawnTime)
        => new DotInfo(random.NextOffset(screenSize), random.NextWithinIntBounds(spawnTime));
}
