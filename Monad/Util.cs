namespace Monad;
static class ExceptionUtil
{
    public static InvalidOperationException Unreachable() => new("unreachable");
}
