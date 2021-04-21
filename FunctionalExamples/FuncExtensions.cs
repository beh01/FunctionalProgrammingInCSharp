using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExamples
{
    public static class FuncExtensions
    {
        public static Func<C> SelectMany<A, B, C>(
            this Func<A> source,
            Func<A, Func<B>> selector,
            Func<A, B, C> resultSelector) => () =>
                {
                    A value = source();
                    return resultSelector(value, selector(value)());
                };

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
