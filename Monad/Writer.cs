using Monoid;
namespace Monad;

public record Writer<W, T>(W Log, T Value)
    where W : IMonoid<W>;

public static class WriterImplementation
{
    public static Writer<W, T> Return<W, T>(T value)
        where W : IMonoid<W>
        => new(W.Empty(), value);

    public static Writer<W, T2> Bind<W, T1, T2>(Writer<W, T1> writer, Func<T1, Writer<W, T2>> f)
        where W : IMonoid<W>
    {
        var (log, value) = f(writer.Value);
        return new Writer<W, T2>(writer.Log.Append(log), value);
    }

}
