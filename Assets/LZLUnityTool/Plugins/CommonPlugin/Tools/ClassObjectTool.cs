using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LZLUnityTool.Plugins.CommonPlugin.Tools
{
    internal class ClassObjectTool<T> where T : new()
    {
        private readonly Stack<T> m_Stack = new Stack<T>();
        private readonly UnityAction<T> m_ActionOnGet;
        private readonly UnityAction<T> m_ActionOnRealse;

        public int countAll { get; private set; }
        public int countActive {
            get
            {
                return countAll - countInactive;
            }
        }
        public int countInactive
        
        {
            get
            {
                return m_Stack.Count;
            }
        }

        public ClassObjectTool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRealse)
        {
            m_ActionOnGet = actionOnGet;
            m_ActionOnRealse = actionOnRealse;
        }

        public T Get()
        {
            T element;
            if (m_Stack.Count == 0)
            {
                element = new T();
                countAll++;
            }
            else
            {
                element = m_Stack.Pop();
            }

            if (m_ActionOnGet!=null)
            {
                m_ActionOnGet(element);
            }

            return element;
        }

        public void Realse(T element)
        {
            if (m_Stack.Count> 0 && ReferenceEquals(m_Stack.Peek(),element))
            {
                Debug.LogError("Internal error.Trying to destroy object that is already released to pool");
            }

            if (m_ActionOnRealse!=null)
            {
                m_ActionOnRealse(element);
            }
            m_Stack.Push(element);
        }
    }

    internal static class ListPool<T>
    {
        private static readonly ClassObjectTool<List<T>> s_ListPool = new ClassObjectTool<List<T>>(null,I=>I.Clear());

        public static List<T> Get()
        {
            return s_ListPool.Get();
        }

        public static void Release(List<T> toRelease)
        {
            s_ListPool.Realse(toRelease);
        }
    }
}