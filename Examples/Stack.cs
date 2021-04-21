using System;
using System.Collections.Generic;
using System.Text;

namespace Examples
{
    public class Stack<T>
    {
        private List<T> data;
        public Stack()
        {
            data = new List<T>();
        }
        public void Push(T item)
        {
            data.Add(item);
        }
        public T Pop()
        {
            T item = data[data.Count-1];
            data.RemoveAt(data.Count-1);
            return item;
        }
    }
}
