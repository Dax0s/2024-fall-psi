namespace backend.Utils;

// Probably gonna need to make some generic version in the future
public struct IntBounds(int lowerLimit, int upperLimit)
{
    public int LowerLimit { get; set; } = lowerLimit;
    public int UpperLimit { get; set; } = upperLimit;

    public bool WithinBounds(int value)
        => LowerLimit <= value && value <= UpperLimit;
}
