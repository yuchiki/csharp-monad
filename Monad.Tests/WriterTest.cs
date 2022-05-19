using Monoid;
using Xunit;

namespace Monad.Tests;

using static WriterImplementation;

public class WriterTest
{
    [Fact]
    public void SatisfyMonadLawOne()
    {
        var x = 2;
        var f = (int y) => new Writer<ListMonoid<string>, int>(new List<string> { "add 10" }.AsListMonoid(), y + 10);

        Assert.Equal(
            Bind(Return<ListMonoid<string>, int>(x), f),
            f(x)
        );
    }

    [Fact]
    public void SatisfyMonadLawTwo()
    {
        var m = new Writer<ListMonoid<string>, int>(new List<string> { "one" }.AsListMonoid(), 1);

        Assert.Equal(
            Bind(m, Return<ListMonoid<string>, int>),
            m
        );
    }

    [Fact]
    public void SatisfyMonadLawThree()
    {
        var m = new Writer<ListMonoid<string>, int>(new List<string> { "one" }.AsListMonoid(), 1);
        var f = (int x) => new Writer<ListMonoid<string>, int>(new List<string> { "+ 10" }.AsListMonoid(), x + 10);
        var g = (int x) => new Writer<ListMonoid<string>, int>(new List<string> { "* 3" }.AsListMonoid(), x * 3);

        Assert.Equal(
            Bind(Bind(m, f), g),
            Bind(m, x => Bind(f(x), g))
        );
    }

    [Fact]
    public void QuerySyntaxWorksAsIntended()
    {
        var mResult =
            from a in new Writer<ListMonoid<string>, int>(new List<string> { "one" }.AsListMonoid(), 1)
            from b in new Writer<ListMonoid<string>, int>(new List<string> { "+ 10" }.AsListMonoid(), b + 10)
            from c in new Writer<ListMonoid<string>, int>(new List<string> { "* 3" }.AsListMonoid(), c * 10)
            from _ Tell ("The result is...")
            select v;
    }
}
