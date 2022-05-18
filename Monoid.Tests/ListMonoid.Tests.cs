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
            Assert.Equal<IEnumerable<int>>(monoid, monoid.AsList().AsListMonoid());
        }
    }
}
