namespace Monad;
using static ExceptionUtil;

public abstract record Maybe<T>();
public sealed record Just<T>(T Value) : Maybe<T>;
public sealed record Nothing<T>() : Maybe<T>;

public static class MaybeImplementation
{
    public static Maybe<T> Return<T>(T value) => new Just<T>(value);
    public static Maybe<T2> Bind<T1, T2>(Maybe<T1> m, Func<T1, Maybe<T2>> f) => m switch
    {
        Nothing<T1> => new Nothing<T2>(),
        Just<T1>(var v) => f(v),
        _ => throw Unreachable()
    };

    public static Maybe<T2> Select<T1, T2>(this Maybe<T1> x, Func<T1, T2> f) =>
        QueryHelper.Select<T1, T2, Maybe<T1>, Maybe<T2>>(Return, Bind)(x, f);

    public static Maybe<T3> SelectMany<T1, T2, T3>(
            this Maybe<T1> x,
            Func<T1, Maybe<T2>> f,
            Func<T1, T2, T3> g
        ) => QueryHelper.SelectMany<T1, T2, T3, Maybe<T1>, Maybe<T2>, Maybe<T3>>(Return, Bind, Bind)(x, f, g);
}
