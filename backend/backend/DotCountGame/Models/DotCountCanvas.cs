using backend.Utils;
namespace backend.DotCountGame.Models;

public class DotCountCanvas
{
    public int SideLength { get; set; }

    public List<Dot> Dots { get; set; }

    // TODO: constants out somewhere else
    // In pixels
    private const int defaultRadius = 10;
    private const int smallRadius = 5;
    private const int dotCountLimitForDefaultRadius = 100;

    // TODO: constants out somewhere else
    private const float MinRadiusPercentage = 0.8f; // What part of max. radius should min. radius be
    private readonly int _maxRadius;
    private readonly int _minRadius;

    public DotCountCanvas(int minDots, int maxDots)
    {
        var dotCount = Random.Shared.Next(minDots, maxDots + 1);
        Dots = new List<Dot>();

        _maxRadius = maxDots < dotCountLimitForDefaultRadius ? defaultRadius : smallRadius;
        _minRadius = Math.Max(1, (int)(_maxRadius * MinRadiusPercentage)); // Max function is so that _minRadius won't be 0

        // See comment above FillInRandomDots() for more info
        var (occupiableChunkSideCount, occupiableChunkCount) = NextPerfectSquare(maxDots);
        SideLength = (2 * _maxRadius) * (2 * occupiableChunkSideCount + 1);

        FillInRandomDots(dotCount, occupiableChunkCount);
    }

    private static (int, int) NextPerfectSquare(int number)
    {
        var nextSquareRoot = (int)Math.Ceiling(Math.Sqrt((double)number));
        return (nextSquareRoot, nextSquareRoot * nextSquareRoot);
    }

    // Fill algorithm
    //
    // Canvas is made of pixel chunks.
    // All chunks are a square with the side lenght equal to the dot diameter.
    // Chunks can be:
    //  - free (dot center WILL NOT be placed in them)
    //  - occupiable (dot center CAN BE placed in them)
    //
    // The if chunk is selected to have a dot center inside,
    // the precise position of that center is calculated as follows:
    //     final_position = chunk_top_left_corner + random_offset
    //
    // Note: free chunks CAN (and in a lot of cases will) contain parts of a dot.
    //
    // Chunk pattern goes as follows:
    //
    // ---------
    // -*-*-*-*-
    // ---------
    // -*-*-*-*-
    // ---------
    // -*-*-*-*-
    // ---------
    // -*-*-*-*-
    // ---------
    //
    // Here '-' marks free chunks and '*' marks occupiable chunks.
    private void FillInRandomDots(int dotCount, int chunkCount)
    {
        int chunkSideLength = 2 * _maxRadius;

        ComputeChunkTopLeftPositions(chunkCount, chunkSideLength);
        ChooseDots(dotCount);
        GiveRandomOffsetsAndRadii(chunkSideLength);
    }

    private void ComputeChunkTopLeftPositions(int chunkCount, int chunkSideLength)
    {
        Dots = new List<Dot>(chunkCount);
        for (int y = chunkSideLength; y < SideLength; y += 2 * chunkSideLength)
        {
            for (int x = chunkSideLength; x < SideLength; x += 2 * chunkSideLength)
            {
                Dots.Add(new Dot(x, y));
            }
        }
    }

    private void ChooseDots(int dotCount)
    {
        var rng = new Random();
        Dots = Dots.OrderBy(_ => rng.Next()).Take(dotCount).ToList();
    }

    private void GiveRandomOffsetsAndRadii(int chunkSideLength)
    {
        foreach (var dot in Dots)
        {
            var randomOffset = new Vector2(
                x: Random.Shared.Next(0, chunkSideLength),
                y: Random.Shared.Next(0, chunkSideLength)
            );

            dot.Center += randomOffset;
            dot.Radius = Random.Shared.Next(_minRadius, _maxRadius + 1);
        }
    }
}
