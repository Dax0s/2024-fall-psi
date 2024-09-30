using backend.Utils;

namespace backend.DotCountGame.Models;

public class Dot
{
    private Vector2 _center;

    public Dot(int x = 0, int y = 0)
    {
        _center = new Vector2(x, y);
    }
    public Dot(Vector2 center)
    {
        _center = center;
    }

    public int X {
        get
        {
            return _center.X;
        }
        set
        {
            _center.X = value;
        }
    }

    public int Y {
        get
        {
            return _center.Y;
        }
        set
        {
            _center.Y = value;
        }
    }
}
