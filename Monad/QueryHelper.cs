using System;
using System.Linq;

namespace Monad;

static class QueryHelper
{
    public static Func<MA, Func<A, B>, MB> Select<A, B, MA, MB>(
        Func<B, MB> ret,
        Func<MA, Func<A, MB>, MB> bind
    ) => (x, f) => bind(x, y => ret(f(y)));

    public static Func<MA, Func<A, MB>, Func<A, B, C>, MC> SelectMany<A, B, C, MA, MB, MC>(
        Func<C, MC> ret,
        Func<MA, Func<A, MC>, MC> bindOuter,
        Func<MB, Func<B, MC>, MC> bindInner
    ) => (x, f, g) =>
        bindOuter(
            x,
            a => bindInner(
                f(a),
                b => ret(g(a, b))
            )
        );
}
