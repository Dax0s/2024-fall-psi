namespace backend.Utils;

public struct Bounds<T>
where T :
        notnull,
        IComparable
{
    public T LowerLimit { get; init; }
    public T UpperLimit { get; init; }

    public Bounds(T lowerLimit, T upperLimit)
        => (LowerLimit, UpperLimit) = lowerLimit.CompareTo(upperLimit) <= 0
                ? (lowerLimit, upperLimit)
                : (upperLimit, lowerLimit);

    public bool WithinBounds(T value)
        => LowerLimit.CompareTo(value) <= 0 && UpperLimit.CompareTo(value) >= 0;
}
