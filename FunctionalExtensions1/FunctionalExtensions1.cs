using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions1
{

    public interface IFunctor<A>
    {
        IApplicative<B> fmap<B>(Func<A, B> f);
    }

    public interface IApplicative<A> : IFunctor<A>
    {
        IApplicative<A> Wrap(A t);
        IApplicative<B> Apply<B>(IApplicative<Func<A, B>> f);
    }
    public interface IMonad<A> : IApplicative<A>
    {
        IMonad<B> Return<B>(B value);
        IMonad<B> Bind<B>(Func<A, IMonad<B>> f);
    }

    public static class FunctionalExtensions
    {
        public static IFunctor<B> fmap<A, B>(this Func<A, B> f, IFunctor<A> a) => a.fmap(f);
        public static IApplicative<B> Apply<A, B>(this IApplicative<Func<A, B>> f, IApplicative<A> a) => a.Apply(f);
        public static IMonad<B> Bind<A, B>(this Func<A, IMonad<B>> f, IMonad<A> a) => a.Bind(f);

        public static IEnumerable<B> Bind<A, B>(this Func<A, IEnumerable<B>> f, IEnumerable<A> a) => a.Bind(f);
        public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> a, Func<A, IEnumerable<B>> f) => a.SelectMany(f);
        public static IEnumerable<A> Return<A>(this A a)
        {
            yield return a;
        }
        public static IEnumerable<B> Bind2<A, B>(this IEnumerable<A> a, Func<A, IEnumerable<B>> f)
        {
            foreach (var itemInA in a)
            {
                foreach (var itemInPartialResult in f(itemInA))
                {
                    yield return itemInPartialResult;
                }
            }
        }
        //  a.Select(itemInA => f(itemInA)).SelectMany(x => x);
        //  (from itemInA in a select (from result in f(itemInA) select result)).SelectMany(x=>x);
        public static IEnumerable<B> Bind3<A, B>(this IEnumerable<A> a, Func<A, IEnumerable<B>> f) =>
            from itemInA in a
            from result in f(itemInA)
            select result;


        public static IEnumerable<C> NewSelectMany<A, B, C>(this IEnumerable<A> a, Func<A, IEnumerable<B>> f, Func<A, B, C> selector)
        {
            foreach (var itemInA in a)
            {
                foreach (var itemInPartialResult in f(itemInA))
                {
                    yield return selector(itemInA, itemInPartialResult);
                }
            }
        }
        public static IEnumerable<B> Bind4<A, B>(this IEnumerable<A> a, Func<A, IEnumerable<B>> f) =>
            a.NewSelectMany(f, (_, x) => x);
    }
}
