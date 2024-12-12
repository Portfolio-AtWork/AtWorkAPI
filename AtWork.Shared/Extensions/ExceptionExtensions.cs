namespace AtWork.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool Ok(this Exception? exception) => exception is null;
    }
}
