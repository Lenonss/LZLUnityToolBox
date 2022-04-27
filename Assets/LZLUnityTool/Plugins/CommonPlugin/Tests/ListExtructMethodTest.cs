using System;
using System.Collections;
using System.Collections.Generic;
using LZLUnityTool.Plugins.CommonPlugin.ExtructFunction;
using Sirenix.OdinInspector;
using UnityEngine;

public class ListExtructMethodTest : MonoBehaviour
{
    [Button("快速排序测试")]
    public void TestQuickSort()
    {
        string str = "";
        List<int> list = new List<int>() {18, 2, 34, 11, 1, 9, 4, 5, 7, 8, 99, 28,8};
        print("start list:");
        str = list.GetPrintStr();
        print(str);
        
        list.QuickSort();
        print("finished quicksort");
        str = list.GetPrintStr();
        print(str);
    }
}
