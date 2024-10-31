using System.Numerics;
using System.Text.Json.Serialization;

namespace backend.Utils;

public struct Vec2<T>
:
    IAdditiveIdentity<Vec2<T>, Vec2<T>>,
    IAdditionOperators<Vec2<T>, Vec2<T>, Vec2<T>>,
    ISubtractionOperators<Vec2<T>, Vec2<T>, Vec2<T>>

where T :
    notnull,
    IFormattable,
    IEquatable<T>,
    IAdditionOperators<T, T, T>,
    IAdditiveIdentity<T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>

{
    [JsonPropertyName("x")]
    public T X { get; set; }
    [JsonPropertyName("y")]
    public T Y { get; set; }

    public Vec2(T x, T y)
        => (X, Y) = (x, y);

    public Vec2(Vec2<T> other) : this(x: other.X, y: other.Y) { }

    public Vec2() : this(AdditiveIdentity) { }

    public static Vec2<T> AdditiveIdentity
    {
        get
        {
            return new Vec2<T>(x: T.AdditiveIdentity, y: T.AdditiveIdentity);
        }
    }

    public static Vec2<T> operator +(Vec2<T> a, Vec2<T> b)
    {
        return new Vec2<T>(x: a.X + b.X, y: a.Y + b.Y);
    }
    public static Vec2<T> operator -(Vec2<T> a, Vec2<T> b)
    {
        return new Vec2<T>(x: a.X - b.X, y: a.Y - b.Y);
    }

    public static Vec2<T> operator *(T scalar, Vec2<T> vector)
    {
        return new Vec2<T>(x: scalar * vector.X, y: scalar * vector.Y);
    }
    public static Vec2<T> operator /(Vec2<T> vector, T denominator)
    {
        return new Vec2<T>(x: vector.X / denominator, y: vector.Y / denominator);
    }
}
