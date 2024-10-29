using backend.Utils;

namespace backend.DotCountGame.Models;

public class Dot
{
    public Vector2 Center { get; set; }
    public int Radius { get; set; }

    public Dot(Vector2 center, int radius = 0)
        => (Center, Radius) = (center, radius);

    public Dot(int x = 0, int y = 0, int r = 0)
        : this(new Vector2(x, y), r) { }
}
