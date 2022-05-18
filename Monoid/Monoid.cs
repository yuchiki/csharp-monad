namespace Monoid;
public interface IMonoid<T>
{
    public static abstract T Empty();
    public abstract T Append(T value);
}
