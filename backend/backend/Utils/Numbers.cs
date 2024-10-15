namespace backend.Utils;

public class Numbers
{
    // Returns (a, b)
    //  * a satisfies a * a == b
    //  * b is the smallest perfect square >= 'number'
    public static (int, int) NextPerfectSquare(int number)
    {
        if (number < 0)
        {
            return (0, 0);
        }
        var nextSquareRoot = (int)Math.Ceiling(Math.Sqrt((double)number));
        return (nextSquareRoot, nextSquareRoot * nextSquareRoot);
    }
}
