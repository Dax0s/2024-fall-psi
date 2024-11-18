using backend.DotCountGame.Data;
using backend.Utils;

namespace backend.DotCountGame.Logic;

public interface IDotCanvasGenerator
{
    public DotCountCanvas GenerateNextCanvas(Bounds<int> dotCountBounds);
}
