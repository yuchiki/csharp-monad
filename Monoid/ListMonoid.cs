using System.Collections;

namespace Monoid;

public sealed record ListMonoid<T>(
    IEnumerable<T> Enumerable
) : IMonoid<ListMonoid<T>>, IEnumerable<T>
{

    public static ListMonoid<T> Empty() =>
        new(new List<T> { });

    public ListMonoid<T> Append(ListMonoid<T> value)
        => new(this.Enumerable.Concat(value.Enumerable));

    public IEnumerator<T> GetEnumerator() =>
        Enumerable.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Equals(ListMonoid<T>? other) =>
        (other is not null) && this.SequenceEqual(other);

    public override int GetHashCode() =>
        Enumerable.Aggregate(0, (x, y) => (y is null) ? x : x ^ y.GetHashCode());

}

public static class ListMonoidExtension
{

    public static List<T> AsList<T>(this ListMonoid<T> m) => m.Enumerable.ToList();
    public static ListMonoid<T> AsListMonoid<T>(this List<T> list) => new(list);
}
