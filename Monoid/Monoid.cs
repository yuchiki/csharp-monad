﻿namespace Monoid;
public interface Monoid<T>
{
    public static T Empty { get; }

    public static T Append(this T self, T value);
}
