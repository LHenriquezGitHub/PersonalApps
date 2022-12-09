using System;

namespace Console.UI
{
    public class Stack
    {
        private object[] stack;

        public Stack()
        {
            Clear();
        }

        public void Push(object obj)
        {
            if (obj == null)
                throw new InvalidOperationException("Cannot insert null values.");

                object[] stackTmp = new object[stack.Length + 1];
                stack.CopyTo(stackTmp, 0);
                stackTmp[stack.Length] = obj;
                stack = stackTmp;
                        
        }

        public object Pop()
        {

            if (stack.Length < 1)
                throw new InvalidOperationException("Stack already empty.");

            object result = stack[stack.Length - 1];

            object[] obj = new object[stack.Length];
            stack.CopyTo(obj, 0);
            stack = new object[stack.Length - 1];
            for (int i = 0; i < obj.Length - 1; i++)
            {
                stack[i] = obj[i];
            }

            return result;
        }


        public object Peek()
        {

            if (stack.Length < 1)
                throw new InvalidOperationException("Stack already empty.");

            return stack[stack.Length - 1];
        }
        public void Clear()
        {
            stack = new object[0];
        }
    }
}