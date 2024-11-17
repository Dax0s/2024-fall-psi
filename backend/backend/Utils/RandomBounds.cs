namespace backend.Utils;

public static class RandomBounds
{
    public static int NextWithinBounds(this Random random, Bounds<int> bounds)
        => Convert.ToInt32(random.NextInt64(
                Convert.ToInt64(bounds.LowerLimit),
                Convert.ToInt64(bounds.UpperLimit) + 1
            ));
}
