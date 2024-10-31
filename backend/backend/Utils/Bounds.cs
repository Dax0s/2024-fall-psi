using System.Runtime.CompilerServices;

namespace backend.Utils;

public struct Bounds<T>(T lowerLimit, T upperLimit)
where T :
        notnull,
        IComparable
{
    public T LowerLimit { get; init; } = lowerLimit;
    public T UpperLimit { get; init; } = upperLimit;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool WithinBounds(T value)
        => LowerLimit.CompareTo(value) >= 0 && UpperLimit.CompareTo(value) < 0;
}
