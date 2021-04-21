using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExamples
{
    public delegate (TState State, T Value) State<TState, T>(TState state);

    public static class StateExtensions
    {
        public static State<TState, C> SelectMany<TState, A, B, C>(
            this State<TState, A> source,
            Func<A, State<TState, B>> selector,
            Func<A, B, C> resultSelector) =>
                oldState =>
                {
                    (TState State, A Value) value = source(oldState);
                    (TState State, B Value) result = selector(value.Value)(value.State);
                    return (result.State, resultSelector(value.Value, result.Value)); 
                };

        public static State<TState, B> Select<TState, A, B>(
            this State<TState, A> source,
            Func<A, B> selector) =>
                oldState =>
                {
                    (TState State, A Value) value = source(oldState);
                    return (value.State, selector(value.Value));
                };

        public static void Test()
        {
            (List<int>, int Max) FindMax(List<int> list)
            {
                int max = list.Max();
                return (list.Where(x => x != max).ToList(), max);
            }
            (List<int>, int Min) FindMin(List<int> list)
            {
                int min = list.Min();
                return (list.Where(x => x != min).ToList(), min);
            }
            State<List<int>, string> query =
                from max in (State<List<int>,int>)(oldState => FindMax(oldState))
                from min in (State<List<int>, int>)(oldState => FindMin(oldState))
                from count in (State<List<int>, int>)(oldState => (null, oldState.Count))
                select $"Max {max}, Min {min}, beside {count} elements.";

            var (State, Value) = query(new List<int>{ 7,1,2,3,5});
            Console.WriteLine(Value);
        }
    }
}
