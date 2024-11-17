using backend.DotCountGame.Data;
using backend.DotCountGame.Settings;
using backend.Utils;

namespace backend.DotCountGame.Models;

public class DotCountCanvas
{
    public int SideLength { get; set; }

    public List<Dot> Dots { get; set; }

    private Bounds<int> _radiusBounds;

    public DotCountCanvas(Bounds<int> dotCountBounds)
    {
        (Dots, SideLength, _radiusBounds) = (new List<Dot>(), 0, new Bounds<int>());

        var dotCount = (new Random()).NextWithinBounds(dotCountBounds);
        CalculateRadiusBounds(dotCountBounds.UpperLimit);
        FillInRandomDots(dotCount, dotCountBounds.UpperLimit);
    }

    private void CalculateRadiusBounds(int maxDots)
    {
        var maxRadius = maxDots < GameSettings.DotCountLimitForDefaultRadius
                ? GameSettings.DefaultRadius
                : GameSettings.SmallRadius;

        var minRadius = (int)(maxRadius * GameSettings.MinRadiusPercentage);
        minRadius = Math.Max(1, minRadius); // So that _minRadius > 0

        _radiusBounds = new Bounds<int>(minRadius, maxRadius);
    }

    // For more info take a look at DotGenerationAlgorithm.md (it's placed in root directory of this game)
    private void FillInRandomDots(int dotCount, int maxDots)
    {
        var (occupiableChunkSideCount, occupiableChunkCount) = Numbers.NextPerfectSquare(maxDots);
        SideLength = (2 * _radiusBounds.UpperLimit) * (2 * occupiableChunkSideCount + 1);

        var chunkSideLength = 2 * _radiusBounds.UpperLimit;

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
        var random = new Random();
        Dots = Dots.OrderBy(_ => random.Next()).Take(dotCount).ToList();
    }

    private void GiveRandomOffsetsAndRadii(int chunkSideLength)
    {
        var random = new Random();
        foreach (var dot in Dots)
        {
            dot.Center = new Vec2<int>(
                random.Next(chunkSideLength + 1),
                random.Next(chunkSideLength + 1)
            );
            dot.Radius = random.NextWithinBounds(_radiusBounds);
        }
    }
}
