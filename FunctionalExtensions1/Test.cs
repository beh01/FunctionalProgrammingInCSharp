using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions1
{
    public static class Test
    {
        public static void TestExtension()
        {
            IFunctor<int> result = new Just<int>() { Value = 1 }
                .fmap(x => x.ToString())
                .fmap(x => int.Parse(x));

            var value = ((Just<int>)result).Value;
            Console.WriteLine(value);

            result = ((Func<int, int>)(x => x + 1)).fmap(new Just<int> { Value = 1 });

            result = new Just<int> { Value = 1 }.fmap(x => x + 1);

            Func<int, Func<int, int>> plus = x => y => x - y;

            result = new Just<int>() { Value = 2 }
                .Apply(new Just<int>() { Value = 1 }
                .Apply(new Just<Func<int, Func<int, int>>>() { Value = plus }));

            value = ((Just<int>)result).Value;
            Console.WriteLine(value);

            result = new Just<int>() { Value = 1 }
                .Apply(new Just<Func<int, Func<int, int>>>() { Value = plus })
                .Apply(new Just<int>() { Value = 2 });

            value = ((Just<int>)result).Value;
            Console.WriteLine(value);

            result = new Just<int>() { Value = 1 }
                .fmap(plus)
                .Apply(new Just<int>() { Value = 2 });

            value = ((Just<int>)result).Value;
            Console.WriteLine(value);

            var result2 = new Just<int>() { Value = 1 }
                .Bind(x => new Just<int> { Value = x + 1 })
                .Bind(x => new Just<string>() { Value = "Value: " + x });

            var value2 = ((Just<string>)result2).Value;
            Console.WriteLine(value2);

            var list = new List<int> { 1, 2, 3 }
                .Bind(x => new List<char> { 'a', 'b' }
                .Bind(ch => new List<(int, char)> { (x, ch) }));
            Console.WriteLine(string.Join(", ", list));

            var list2 = new List<int> { 1, 2, 3 }
                .Bind(x => new List<int> { x+1 });
            Console.WriteLine(string.Join(", ", list2));

            list = new List<int> { 1, 2, 3 }
                .Bind2(x => new List<char> { 'a', 'b' }
                .Bind2(ch => new List<(int, char)> { (x, ch) }));
            Console.WriteLine(string.Join(", ", list));

            list = new List<int> { 1, 2, 3 }
                .Bind3(x => new List<char> { 'a', 'b' }
                .Bind3(ch => new List<(int, char)> { (x, ch) }));
            Console.WriteLine(string.Join(", ", list));

            list = new List<int> { 1, 2, 3 }
                .Bind4(x => new List<char> { 'a', 'b' }
                .Bind4(ch => new List<(int, char)> { (x, ch) }));
            Console.WriteLine(string.Join(", ", list));

            list = new List<int> { 1, 2, 3 }.SelectMany<int, char, (int, char)>(
                 x => new List<char> { 'a', 'b' },
                 (y, ch) => (y, ch));
            Console.WriteLine(string.Join(", ", list));

            list2 = new List<int> { 1, 2, 3 }.SelectMany(
                 x => new List<int>{ x + 1 },
                 (_,y) => y);
            Console.WriteLine(string.Join(", ", list));


            var test =
                from x in new Just<int> { Value = 1 }
                from y in new Just<int> { Value = 1 }
                from z in new Just<int> { Value = 1 }
                select x + y + z;

            value = ((Just<int>)test).Value;
            Console.WriteLine("from test:"+value);

        }
    }
}
