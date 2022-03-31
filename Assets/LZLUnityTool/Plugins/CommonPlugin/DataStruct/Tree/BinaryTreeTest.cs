using System.Collections.Generic;
using UnityEngine;

namespace LZLUnityTool.Plugins.CommonPlugin.DataStruct.Tree
{
    public class BinaryTreeTest : MonoBehaviour
    {
        [ContextMenu("前序遍历测试")]
        public void TestSortBinaryTree()
        {
            List<int> list = new List<int>() {10, 1, 3, 5, 6, 12, 14, 15, 7, 8};
            SortBinaryTree<int> tree = new SortBinaryTree<int>();
            foreach (var i in list)
            {
                tree.Insert(i);
            }

            List<int> preOrder = tree.PreOrderTree();
            Debug.Log("前序遍历结果");
            foreach (var item in preOrder)
            {
                Debug.Log(item);
            }
        }
    }
}