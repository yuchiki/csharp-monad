using System.Linq;
using Xunit;

namespace Monoid.Tests;

public class ListMonoidTests
{
    [Fact]
    public void AsListMonoidAsListIsIdentity()
    {
        var lists = new List<List<int>>{
            new List<int> {},
            new List<int> {1, 2, 3}
        };

        foreach (var list in lists)
        {
            Assert.Equal(list, list.AsListMonoid().AsList());
        }
    }

    [Fact]
    public void AsListAsListMonoidIsIdentity()
    {
        var listMonoids = new List<ListMonoid<int>>{
            new ListMonoid<int>(new List<int> {}),
            new ListMonoid<int>(new List<int> {1,2,3})
        };

        foreach (var monoid in listMonoids)
        {
            Assert.Equal(monoid, monoid.AsList().AsListMonoid());
        }
    }

    [Fact]
    public void EmptyIsEmpty()
    {
        var empty = ListMonoid<int>.Empty();

        var monoids = new List<ListMonoid<int>> {
            new List<int> { }.AsListMonoid(),
            new List<int> { 1, 2, 3 }.AsListMonoid()
        };

        foreach (var monoid in monoids)
        {
            Assert.Equal(empty.Append(monoid), monoid);
            Assert.Equal(monoid.Append(empty), monoid);
        }
    }
}
