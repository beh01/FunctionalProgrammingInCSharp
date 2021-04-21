using Examples;
using FunctionalExamples;
using System;

namespace Main
{
    class Program
    {
        public static void StackTest()
        {
            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            var x = stack.Pop();
            Console.WriteLine(x);

            NewStack<int> newStack = NewStack<int>.Empty();
            newStack = NewStack<int>.Push(newStack, 1);
            newStack = NewStack<int>.Push(newStack, 2);

            (x,newStack) = NewStack<int>.Pop(newStack);

            Console.WriteLine(x);
        }

        static void Main(string[] args)
        {
            RangeClient.Test();
            StackTest();
            FunctionalExtensions1.Test.TestExtension();
            FunctionalExtensions2.Test.TestExtension();
            //requires user input
            //LazyExtensions.Test();
            //FuncExtensions.Test();
            //IOExtensions.Test();

            StateExtensions.Test();
        }
    }
}
