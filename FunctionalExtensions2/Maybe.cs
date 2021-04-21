namespace FunctionalExtensions2
{
    public abstract class Maybe<T>
    {
    }
    public class Just<T> : Maybe<T>
    {
        public T Value { get; init; }
    }
    public class Nothing<T> : Maybe<T>
    {
    }
}
