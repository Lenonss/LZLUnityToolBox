using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LZLUnityTool.Plugins.CommonPlugin.DataStruct.Tree
{
    public class BinaryTree<T> where  T :IComparable
    {
        public TreeNode<T> root;

        public TreeNode<T> leftTreeRoot;
        public TreeNode<T> rightTreeNode;

        public  BinaryTree()
        {

        }
    }

    public class SortBinaryTree<T1> : BinaryTree<T1> where T1:IComparable
    {
        public SortBinaryTree()
        {
            root = null;
        }

        public SortBinaryTree(T1 data)
        {
            root = new TreeNode<T1>(data);
        }

        /// <summary>
        /// 插入节点，小于根节点的值在根节点左侧
        /// </summary>
        /// <param name="data"></param>
        public void Insert(T1 data)
        {
            if (root == null)
            {
                root = new TreeNode<T1>(data);
                return;
            }
            root.Insert(data);
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        public List<T1> PreOrderTree()
        {
            if (root == null)
            {
                return null;
            }

            List<T1> result = new List<T1>();
            PreOrderTree(result, root);
            return result;
        }
        private void PreOrderTree(List<T1> list, TreeNode<T1> node)
        {
            if (node == null)
            {
                return;
            }
            list.Add(node.data);
            PreOrderTree(list, node.leftChild);
            PreOrderTree(list, node.rightChild);
        }
    }
}

