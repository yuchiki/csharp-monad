using Microsoft.VisualBasic;
using Xunit;
using Xunit.Sdk;

namespace MonadLibrary.Tests;

using static MaybeImplementation;

public class MonadLibraryTest
{
    [Fact]
    public void SatisfyMonadLawOne()
    {
        var x = 2;
        var f = (int y) => new Just<int>(y * 10);
        var g = (int _) => new Nothing<int>();

        Assert.Equal(
            Bind(Return(x), f),
            f(x)
        );

        Assert.Equal(
            Bind(Return(x), g),
            g(x)
        );
    }

    [Fact]
    public void SatisfyMonadLawTwo()
    {
        var m1 = new Just<int>(1);
        var m2 = new Nothing<int>();

        Assert.Equal(
            Bind(m1, Return<int>),
            m1
        );

        Assert.Equal(
            Bind(m2, Return<int>),
            m2
        );
    }

    [Fact]
    public void SatisfyMonadLawThree()
    {
        var m1 = new Just<int>(1);
        var m2 = new Nothing<int>();

        var f = (int x) => new Just<int>(x * 2);
        var g = (int y) => new Just<int>(y + 10);

        Assert.Equal(
            Bind(Bind(m1, f), g),
            Bind(m1, x => Bind(f(x), g))
        );

        Assert.Equal(
            Bind(Bind(m2, f), g),
            Bind(m2, x => Bind(f(x), g))
        );
    }

    [Fact]
    public void ReturnsJustWhenAllInputsAreJust()
    {
        var ma = new Just<int>(1);
        var mb = new Just<int>(2);
        var mc = new Just<int>(3);

        var mResult =
            from a in ma
            from b in mb
            from c in mc
            select (a, b, c);

        Assert.Equal(mResult, new Just<(int, int, int)>((1, 2, 3)));
    }


    [Fact]
    public void ReturnsNothingWhenSomeInputsAreNothing()
    {
        var ma = new Just<int>(1);
        var mb = new Just<int>(2);
        var mc = new Nothing<int>();
        var md = new Just<int>(4);

        var mResult =
            from a in ma
            from b in mb
            from c in mc
            from d in md
            select (a, b, c, d);

        Assert.Equal(mResult, new Nothing<(int, int, int, int)>());
    }
}
