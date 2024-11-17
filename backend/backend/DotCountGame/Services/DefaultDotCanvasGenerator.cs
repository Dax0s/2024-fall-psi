using System.Runtime.InteropServices;
using System.Security.Cryptography;

using backend.DotCountGame.Data;
using backend.DotCountGame.Settings;
using backend.Utils;

namespace backend.DotCountGame.Services;

public class DefaultDotCanvasGenerator : IDotCountGameInfoGenerator
{
    private int _sideLength;
    private List<Dot> _dots;

    public DefaultDotCanvasGenerator()
        => (_sideLength, _dots) = (0, new List<Dot>());

    public DotCountCanvas GenerateNextCanvas(Bounds<int> dotCountBounds)
    {
        FillInRandomDots(dotCountBounds);
        return new DotCountCanvas(_sideLength, _dots);
    }

    // For more info take a look at DotGenerationAlgorithm.md (it's placed in root directory of this game)
    private void FillInRandomDots(Bounds<int> dotCountBounds)
    {
        var dotCount = (new Random()).NextWithinBounds(dotCountBounds);
        var radiusBounds = GameSettings.GetRadiusBounds(dotCountBounds.UpperLimit);

        var chunkSideLength = 2 * radiusBounds.UpperLimit;
        var (occupiableChunkSideCount, occupiableChunkCount) = Numbers.NextPerfectSquare(dotCountBounds.UpperLimit);

        _sideLength = chunkSideLength * (2 * occupiableChunkSideCount + 1);
        _dots = new List<Dot>(occupiableChunkCount);

        ChooseDots(dotCount, chunkSideLength);
        GiveRandomOffsetsAndRadii(chunkSideLength, radiusBounds);
    }

    private void ChooseDots(int dotCount, int chunkSideLength)
    {
        for (int y = chunkSideLength; y < _sideLength; y += 2 * chunkSideLength)
        {
            for (int x = chunkSideLength; x < _sideLength; x += 2 * chunkSideLength)
            {
                _dots.Add(new Dot(x, y));
            }
        }

        RandomNumberGenerator.Shuffle(CollectionsMarshal.AsSpan(_dots));
        _dots = _dots.Take(dotCount).ToList();
    }

    private void GiveRandomOffsetsAndRadii(int chunkSideLength, Bounds<int> radiusBounds)
    {
        var random = new Random();
        foreach (var dot in _dots)
        {
            dot.Center += new Vec2<int>(
                random.Next(chunkSideLength + 1),
                random.Next(chunkSideLength + 1)
            );
            dot.Radius = random.NextWithinBounds(radiusBounds);
        }
    }
}
