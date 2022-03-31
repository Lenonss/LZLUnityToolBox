

namespace LZLUnityTool.Plugins.CommonPlugin.DataStruct
{
    public class ListNode<T>
    {
        public T value;
        public ListNode<T> next;

        public ListNode(T v)
        {
            this.value = v;
        }
    }
}

