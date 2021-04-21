using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExamples
{
    public delegate T IO<out T>();

    public enum Unit
    {
        Unit
    }

    public static class IOExtensions
    {
        public static IO<C> SelectMany<A, B, C>(
           this IO<A> source,
           Func<A, IO<B>> selector,
           Func<A, B, C> resultSelector) =>
               () =>
               {
                   A value = source();
                   return resultSelector(value, selector(value)());
               };
        public static IO<A> Return<A>(this A value) => () => value;
        public static IO<B> Select<A, B>(
            this IO<A> source,
            Func<A, B> selector) =>
                source.SelectMany(
                    value => Return(selector(value)), 
                    (_, result) => result);

        public static IO<TResult> IO<TResult>(Func<TResult> function) =>
            () => function();

        public static IO<Unit> IO(Action action) =>
            () =>
            {
                action();
                return default;
            };

        public static void Test()
        {
            IO<string> query =
                from unit1 in IO(() => Console.WriteLine("File path:")) // IO<Unit>.
                from filePath in IO(Console.ReadLine) // IO<string>.
                let encoding = Encoding.GetEncoding("utf-16")
                from fileContent in IO(() => File.ReadAllText(filePath, encoding)) // IO<string>.
                from unit3 in IO(() => Console.WriteLine("File content:")) // IO<Unit>.
                from unit4 in IO(() => Console.WriteLine(fileContent)) // IO<Unit>.
                select fileContent; // Define query.
            Console.WriteLine("Query created.");
            var file = query(); // Execute query.
        }
    }
}
