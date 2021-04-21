using System;
using System.IO;

namespace FunctionalExamples
{
    public static partial class LazyExtensions
    {
        public static Lazy<A> Return<A>(A value) => new(value);
       
        public static Lazy<C> SelectMany<A, B, C>(
            this Lazy<A> source,
            Func<A, Lazy<B>> selector,
            Func<A, B, C> resultSelector) =>
                new Lazy<C>(() => resultSelector(source.Value, selector(source.Value).Value));

        public static void Test()
        {
            Lazy<string> query = from name in new Lazy<string>(Console.ReadLine)
                                 from fileContent in new Lazy<string>(() => File.ReadAllText(name))
                                 select fileContent;
            Console.WriteLine("Query created.");
            string result = query.Value; // Execute query.
            Console.WriteLine(result);
        }
    }
}
