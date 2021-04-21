using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuePresentation
{
    static class CompositionExtensions
    {
        public static Func<A, C> After<A, B, C>(this Func<B, C> f, Func<A, B> g) => value => f(g(value));
        public static Func<A,C> Composition<A, B, C>(Func<B, C> f, Func<A, B> g) => value => f(g(value));

        public static void Test()
        {
            Func<string, int> parse = int.Parse; // string -> int
            Func<int, int> abs = Math.Abs; // int -> int

            Func<string, int> composition1 = abs.After(parse);
            Func<string, int> composition2 = Composition(abs, parse);

        }
    }
}
