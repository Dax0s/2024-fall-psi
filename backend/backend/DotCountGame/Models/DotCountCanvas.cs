using backend.Properties;
using backend.Utils;

namespace backend.DotCountGame.Models;

public class DotCountCanvas
{
    public int SideLength { get; set; }

    public List<Dot> Dots { get; set; }

    private IntBounds _radiusBounds;

    public DotCountCanvas(IntBounds dotCountBounds)
    {
        Dots = new List<Dot>();
        SideLength = 0;
        _radiusBounds = new IntBounds(0, 0);

        var dotCount = dotCountBounds.Random();
        CalculateRadiusBounds(dotCountBounds.UpperLimit);
        FillInRandomDots(dotCount, dotCountBounds.UpperLimit);
    }

    private void CalculateRadiusBounds(int maxDots)
    {
        var maxRadius = maxDots < Settings.DotCountGame.DotCountLimitForDefaultRadius
                ? Settings.DotCountGame.DefaultRadius
                : Settings.DotCountGame.SmallRadius;

        var minRadius = (int)(maxRadius * Settings.DotCountGame.MinRadiusPercentage);
        minRadius = Math.Max(1, minRadius); // So that _minRadius > 0

        _radiusBounds = new IntBounds(minRadius, maxRadius);
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
    private void FillInRandomDots(int dotCount, int maxDots)
    {
        var (occupiableChunkSideCount, occupiableChunkCount) = Numbers.NextPerfectSquare(maxDots);
        SideLength = (2 * _radiusBounds.UpperLimit) * (2 * occupiableChunkSideCount + 1);

        int chunkSideLength = 2 * _radiusBounds.UpperLimit;

        ComputeChunkTopLeftPositions(occupiableChunkCount, chunkSideLength);
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
        var random = new Random();
        var offsetBounds = new Vector2(chunkSideLength, chunkSideLength);
        foreach (var dot in Dots)
        {
            dot.Center += random.NextOffset(offsetBounds);
            dot.Radius = _radiusBounds.Random();
        }
    }
}
