using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions2
{
    public static class Test
    {
        public static void TestExtension()
        {
            Maybe<int> result = new Just<int>() { Value = 1 }
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


        }
    }
}
