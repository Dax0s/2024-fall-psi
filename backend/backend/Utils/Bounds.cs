namespace backend.Utils;

public struct Bounds<T>(T lowerLimit, T upperLimit)
where T :
        notnull,
        IComparable
{
    public T LowerLimit { get; init; } = lowerLimit;
    public T UpperLimit { get; init; } = upperLimit;

    public bool WithinBounds(T value)
        => LowerLimit.CompareTo(value) <= 0 && UpperLimit.CompareTo(value) >= 0;
}

public static partial class RandomExtentions
{
    public static int NextWithinBounds(this Random random, Bounds<int> bounds)
        => bounds.LowerLimit > bounds.UpperLimit
            ? 0
            : random.Next(bounds.LowerLimit, bounds.UpperLimit + 1);
}
