namespace backend.Utils;

public class Numbers
{
    // Returns (a, b)
    //  * a satisfies a * a == b
    //  * b is the smallest perfect square >= 'number'
    public static (int, int) NextPerfectSquare(int number)
    {
        var nextSquareRoot = number > 0 ? (int)Math.Ceiling(Math.Sqrt((double)number)) : 0;
        return (nextSquareRoot, nextSquareRoot * nextSquareRoot);
    }
}
