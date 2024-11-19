namespace backend.Utils;

public static class RandomVec2
{
    public static Vec2<uint> NextOffset(this Random random, Vec2<uint> maxOffset)
        => new Vec2<uint>(
            Convert.ToUInt32(random.NextInt64(Convert.ToInt64(maxOffset.X))),
            Convert.ToUInt32(random.NextInt64(Convert.ToInt64(maxOffset.Y)))
        );
}
