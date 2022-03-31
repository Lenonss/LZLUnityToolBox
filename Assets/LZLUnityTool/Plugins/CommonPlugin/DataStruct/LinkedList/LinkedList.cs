namespace LZLUnityTool.Plugins.CommonPlugin.DataStruct
{
    public class LinkedList<T>
    {
        public ListNode<T> Head;
        public int Length;

        public LinkedList()
        {
            Head = null;
            Length = 0;
        }

        public LinkedList(ListNode<T> node)
        {
            Head = node;
            Length = 1;
        }

        public LinkedList(T data)
        {
            Head = new ListNode<T>(data);
            Length = 1;
        }
        
        /// <summary>
        /// 在末尾添加一个节点
        /// </summary>
        /// <param name="node">节点</param>
        public void Add(ListNode<T> node)
        {
            if (Head == null)
            {
                Head = node;
                Length = 1;
                return;
            }

            ListNode<T> cur = Head;
            while (cur.next != null)
            {
                cur = cur.next;
            }

            cur.next = node;
            Length++;
        }

        /// <summary>
        /// 在末尾添加一个节点
        /// </summary>
        /// <param name="data">节点值</param>
        /// <returns></returns>
        public ListNode<T> Add(T data)
        {
            ListNode<T> node = new ListNode<T>(data);
            Add(node);
            return node;
        }

        /// <summary>
        /// 反转链表
        /// </summary>
        public void Reverse()
        {
            if (Head == null)
            {
                return;
            }

            ListNode<T> pre = null, cur = Head, nex = null;
            while (cur != null)
            {
                nex = cur.next;//记录cur节点之后的链表，cur节点就独立出来了
                cur.next = pre;//指向逆置链表的第一个节点
                pre = cur;//逆置链表第一个节点更新
                cur = nex;//cur更新位置
            }

            Head = pre;//更新头节点
        }
        /// <summary>
        /// 反转链表
        /// </summary>
        /// <param name="node">链表头节点</param>
        /// <returns>反转后的链表头节点</returns>
        public ListNode<T> Reverse(ListNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            ListNode<T> pre = null, cur = node, nex = null;
            while (cur != null)
            {
                nex = cur.next;
                cur.next = pre;
                pre = cur;
                cur = nex;
            }
            return pre;
        }

        public ListNode<T> Reverse(int start, int end)
        {
            if (start > end || start < 0 || end < 0 || end > Length)
            {
                return null;
            }

            ListNode<T> dummyNode = new ListNode<T>(default);
            dummyNode.next = Head;
            ListNode<T> pre = dummyNode;

            ListNode<T> leftNode, rightNode;
            for (int i = 0; i < start - 1; i++)
            {
                pre = pre.next;
            }
            leftNode = pre.next;

            rightNode = pre;
            for (int i = 0; i < end - (start - 1); i++)
            {
                rightNode = rightNode.next;
            }

            ListNode<T> cur = rightNode.next;//右侧的链表
            //pre为左侧链表的连接点
            
            rightNode.next = null;
            pre.next = null;
            
            Reverse(leftNode);

            pre.next = rightNode;//right为截出链表的头节点
            leftNode.next = cur;//left为截出链表的尾节点

            Head = dummyNode.next;
            return Head;
        }
    }
}