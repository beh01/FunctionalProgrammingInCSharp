using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions2
{
    public static class FunctionalExtensions
    {
        public static Maybe<B> fmap<A, B>(this Maybe<A> x, Func<A, B> f)
            => x switch
            {
                Nothing<A> => new Nothing<B>(),
                Just<A> v => new Just<B>() { Value = f(v.Value) },
                _ => throw new Exception("Unexpected value.")
            };
        public static Maybe<B> fmap<A, B>(this Func<A, B> f, Maybe<A> a) => a.fmap(f);
        public static Maybe<B> Apply<A, B>(this Maybe<A> x, Maybe<Func<A, B>> f)
            => f switch
            {
                Nothing<Func<A, B>> => new Nothing<B>(),
                Just<Func<A, B>> justF => x.fmap(justF.Value),
                _ => throw new Exception("Unexpected value.")
            };
        public static Maybe<B> Apply<A, B>(this Maybe<Func<A, B>> f, Maybe<A> a) => a.Apply(f);
        public static Maybe<A> Wrap<A>(this A value) => new Just<A> { Value = value };
        public static Maybe<A> Return<A>(this A value) => value.Wrap();
        public static Maybe<B> Bind<A, B>(this Maybe<A> x, Func<A, Maybe<B>> f) => x switch
        {
            Nothing<A> => new Nothing<B>(),
            Just<A> value => f(value.Value),
            _ => throw new Exception("Unexpected value.")
        };
        public static Maybe<B> Bind<A, B>(this Func<A, Maybe<B>> f, Maybe<A> a) => a.Bind(f);
    }
}
