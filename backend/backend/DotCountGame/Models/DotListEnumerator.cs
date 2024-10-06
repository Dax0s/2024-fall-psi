using System.Collections;

namespace backend.DotCountGame.Models;

public class DotListEnumerator : IEnumerator
{
    public Dot[] dots;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    private int _position;

    public DotListEnumerator(Dot[] list)
    {
        dots = list;
        Reset();
    }

    public bool MoveNext()
    {
        return ++_position < dots.Length;
    }

    public void Reset()
    {
        _position = 0;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public Dot Current
    {
        get
        {
            try
            {
                return dots[_position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
        set
        {
            try
            {
                dots[_position] = value;
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
