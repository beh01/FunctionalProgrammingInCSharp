using System;

namespace FunctionalExtensions1
{
    /* Applicattive version
    public abstract class Maybe<A> : IApplicative<A>
    {
        public abstract IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f);
        public IApplicative<B> fmap<B>(Func<A, B> f) => Apply<B>(new Just<Func<A,B>>() { Value = f });
        public IApplicative<A> Wrap(A b) => new Just<A>() { Value = b };
    }
    public class Just<T> : Maybe<T>
    {
        public T Value { get; init; }

        public override IApplicative<B> Apply<B>(IApplicative<Func<T, B>> f) => f switch
        {
            Just<Func<T, B>> justF => new Just<B>() { Value = justF.Value(Value) },
            _ => new Nothing<B>()
        };

        //public override IApplicative<TResult> fmap<TResult>(Func<T, TResult> f) => new Just<TResult>() { Value = f(Value) };
    }
    public class Nothing<T> : Maybe<T>
    {
        public override IApplicative<B> Apply<B>(IApplicative<Func<T, B>> f) => new Nothing<B>();
    }
    */
    public abstract class Maybe<A> : IMonad<A>
    {
        public IApplicative<B> fmap<B>(Func<A, B> f) => Bind(x => Return(f(x)));
        public IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f) => 
            ((IMonad<Func<A, B>>)f).Bind(f=>this.Bind(x => Return(f(x))));
        public IApplicative<A> Wrap(A value) => Return(value);
        public IMonad<B> Return<B>(B value) => new Just<B>() { Value = value };
        abstract public IMonad<B> Bind<B>(Func<A, IMonad<B>> f);

        public Maybe<B> SelectMany<B>(Func<A, Maybe<B>> f) => (Maybe<B>)Bind(f);
        public Maybe<C> SelectMany<B, C>(Func<A, IMonad<B>> f, Func<A, B, C> resultSelector)
        {
            Maybe<B> value = (Maybe<B>)this.Bind(f);
            return value switch
            {
                Just<B> result when this is Just<A> =>
                    new Just<C> { Value = resultSelector(((Just<A>)this).Value, result.Value) },
                _ => new Nothing<C>()
            };
        }
        public Maybe<C> SelectMany2<B, C>(Func<A, IMonad<B>> f, Func<A, B, C> resultSelector) =>
            (Maybe<C>)Bind(x => f(x).Bind(y => Return(resultSelector(x, y))));

    }
    public class Just<T> : Maybe<T>
    {
        public T Value { get; init; }
        public override IMonad<B> Bind<B>(Func<T, IMonad<B>> f) => f(Value);
    }
    public class Nothing<T> : Maybe<T>
    {
        public override IMonad<B> Bind<B>(Func<T, IMonad<B>> f) => new Nothing<B>();
    }
}
