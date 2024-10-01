namespace backend.DotCountGame.Models;

public class DotCountCanvas
{
    public int CanvasWidth { get; set; }

    public List<Dot> Dots { get; set; }

    public int DotRadius { get; set; }

    public DotCountCanvas(int minDots, int maxDots)
    {
        int dotCount = Random.Shared.Next(minDots, maxDots + 1);
        Dots = new List<Dot>(dotCount);

        // See comment above FillInRandomDots() for more info
        int occupiableChunkCount = NextPerfectSquare(maxDots);
        int sideOccupiableChunkCount = (int)Math.Sqrt(occupiableChunkCount);

        DotRadius = maxDots < 100 ? 10 : 5;
        CanvasWidth = (2 * DotRadius) * (2 * sideOccupiableChunkCount + 1);

        FillInRandomDots(dotCount, occupiableChunkCount);
    }

    private static int NextPerfectSquare(int number)
    {
        int nextSquareRoot = (int)Math.Ceiling(Math.Sqrt((double)number));
        return nextSquareRoot * nextSquareRoot;
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
        int chunkWidth = 2 * DotRadius;

        var chunkTopLefts = new Dot[chunkCount];
        var chunkTopLeftsEnum = new DotListEnumerator(chunkTopLefts);
        for (int y = chunkWidth; y < CanvasWidth; y += 2 * chunkWidth)
        {
            for (int x = chunkWidth; x < CanvasWidth; x += 2 * chunkWidth)
            {
                chunkTopLeftsEnum.Current = new Dot(x, y);
                chunkTopLeftsEnum.MoveNext();
            }
        }

        Random.Shared.Shuffle(chunkTopLefts);

        foreach (Dot topLeftDot in chunkTopLefts)
        {
            if (Dots.Count >= dotCount)
            {
                return;
            }

            Dot newDot = topLeftDot;
            newDot.X += Random.Shared.Next(0, chunkWidth);
            newDot.Y += Random.Shared.Next(0, chunkWidth);

            Dots.Add(newDot);
        }
    }
}
