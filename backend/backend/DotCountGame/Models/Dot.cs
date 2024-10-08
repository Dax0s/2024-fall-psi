using backend.Utils;

namespace backend.DotCountGame.Models;

public class Dot
{
    private Vector2 _center;
    private int _radius;

    public Dot(int x = 0, int y = 0, int r = 0)
    {
        _center = new Vector2(x, y);
        _radius = r;
    }
    public Dot(Vector2 center, int radius = 0)
    {
        _center = center;
        _radius = radius;
    }

    public Vector2 Center
    {
        get
        {
            return _center;
        }
        set
        {
            _center = value;
        }
    }

    public int Radius
    {
        get
        {
            return _radius;
        }
        set
        {
            _radius = value;
        }
    }
}
