using backend.DotCountGame.Data;
using backend.Utils;

namespace backend.DotCountGame.Services;

public interface IDotCountGameInfoGenerator
{
    public DotCountCanvas GenerateNextCanvas(Bounds<int> dotCountBounds);
}
