using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomQueue <T>
{
    public Stack<T> stack1;
    public Stack<T> stack2;

    public CustomQueue()
    {
        stack1 = new Stack<T>();
        stack2 = new Stack<T>();
    }

    public void Push(T data)
    {
        stack1.Push(data);
    }

    public T  Pop()
    {
        T result = default;
        if (stack2.Count > 0)
        {
            result = stack2.Pop();
        }
        else
        {
            while (stack1.Count > 0)
            {
                stack2.Push(stack1.Pop());
            }

            result = stack2.Pop();
        }

        return result;
    }
}
