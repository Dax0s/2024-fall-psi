using backend.Utils;

namespace backend.DotCountGame.Models;

public class Dot
{
    public Vec2<int> Center { get; set; }
    public int Radius { get; set; }

    public Dot(Vec2<int> center, int radius = 0)
        => (Center, Radius) = (center, radius);

    public Dot(int x = 0, int y = 0, int r = 0)
        : this(new Vec2<int>(x, y), r) { }
}
