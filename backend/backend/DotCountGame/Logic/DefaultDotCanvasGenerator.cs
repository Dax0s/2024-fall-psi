using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using backend.DotCountGame.Data;
using backend.DotCountGame.Settings;
using backend.Utils;

namespace backend.DotCountGame.Logic;

public class DefaultDotCanvasGenerator : IDotCanvasGenerator
{
    private int _sideLength;
    private List<Dot> _dots;

    public DefaultDotCanvasGenerator()
        => (_sideLength, _dots) = (0, new List<Dot>());

    // For more info take a look at DotGenerationAlgorithm.md (it's placed in root directory of this game)
    public DotCountCanvas GenerateNextCanvas(Bounds<int> dotCountBounds)
    {
        var dotCount = (new Random()).NextWithinBounds(dotCountBounds);
        var radiusBounds = GameSettings.GetRadiusBounds(dotCountBounds.UpperLimit);

        var chunkSideLength = 2 * radiusBounds.UpperLimit;
        var (occupiableChunkSideCount, occupiableChunkCount) = Numbers.NextPerfectSquare(dotCountBounds.UpperLimit);

        _sideLength = chunkSideLength * (2 * occupiableChunkSideCount + 1);
        _dots = new List<Dot>(occupiableChunkCount);

        Task.Run(() => GetRandomDots(dotCount, chunkSideLength, radiusBounds)).Wait();

        return new DotCountCanvas(_sideLength, _dots);
    }

    private void FillRow(ConcurrentBag<Dot> dotBag, int y, int chunkSideLength, Bounds<int> radiusBounds)
    {
        var random = new Random();
        for (int x = chunkSideLength; x < _sideLength; x += 2 * chunkSideLength)
        {
            var center = new Vec2<int>(
                x + random.Next(chunkSideLength + 1),
                y + random.Next(chunkSideLength + 1)
            );
            var radius = random.NextWithinBounds(radiusBounds);

            dotBag.Add(new Dot(center, radius));
        }
    }

    private async Task GetRandomDots(int dotCount, int chunkSideLength, Bounds<int> radiusBounds)
    {
        ConcurrentBag<Dot> dotBag = new ConcurrentBag<Dot>();
        List<Task> tasks = new List<Task>();

        for (int y = chunkSideLength; y < _sideLength; y += 2 * chunkSideLength)
        {
            var yValue = y;
            tasks.Add(Task.Run(() => FillRow(dotBag, yValue, chunkSideLength, radiusBounds)));
        }

#pragma warning disable CA2007
        await Task.WhenAll(tasks);
#pragma warning restore CA2007

        var randomDots = dotBag.ToList();
        RandomNumberGenerator.Shuffle(CollectionsMarshal.AsSpan(randomDots));
        _dots = randomDots.Take(dotCount).ToList();
    }
}
