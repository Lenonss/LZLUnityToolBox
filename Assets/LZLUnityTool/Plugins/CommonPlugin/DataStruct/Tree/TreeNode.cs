
using System;
using System.Collections.Generic;

namespace LZLUnityTool.Plugins.CommonPlugin.DataStruct.Tree
{
    public partial class TreeNode<T>where T : IComparable
    {
        //节点下标
        public int index;
        public T data;

        public TreeNode<T> leftChild;
        public TreeNode<T> rightChild;
        
        public TreeNode()
        {
        }

        public TreeNode(T d)
        {
            this.data = d;
            leftChild = null;
            rightChild = null;
        }

        public void Insert(T data) 
        {
            T curNodeVal = this.data;
            if (curNodeVal.CompareTo(data) > 0)
            {
                if (leftChild == null)
                {
                    leftChild = new TreeNode<T>(data);
                }
                else
                {
                    leftChild.Insert(data);
                }
            }
            else
            {
                if (rightChild == null)
                {
                    rightChild = new TreeNode<T>(data);
                }
                else
                {
                    rightChild.Insert(data);
                }
            }
        }
        
    }
}