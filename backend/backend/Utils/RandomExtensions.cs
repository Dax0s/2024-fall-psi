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

    public static Vec2<int> NextOffset(this Random random, Vec2<int> maxOffset)
        => maxOffset.X < 0 || maxOffset.Y < 0
            ? new Vec2<int>(0, 0)
            : new Vec2<int>(random.Next(maxOffset.X), random.Next(maxOffset.Y));

    public static DotInfo NextDotInfo(this Random random, Vec2<int> screenSize, Bounds<int> spawnTime)
        => new DotInfo(random.NextOffset(screenSize), random.NextWithinIntBounds(spawnTime));
}
