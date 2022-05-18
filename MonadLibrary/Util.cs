namespace MonadLibrary;
static class ExceptionUtil
{
    public static InvalidOperationException Unreachable() => new("unreachable");
}
