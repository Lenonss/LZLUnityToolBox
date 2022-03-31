using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace LZLUnityTool.Plugins.CommonPlugin.ExtructFunction
{
    public static class LZLListTool
    {
        /// <summary>
        /// 获得数组的随机项
        /// </summary>
        /// <param name="srcList"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static TValue GetRandomItem<TValue>(this List<TValue> srcList)
        {
            if (srcList.Count == 0)
            {
                return default;
            }
            int randomTemp = Random.Range(0, srcList.Count);
            return  srcList[randomTemp];
        }
        
        /// <summary>
        /// 打乱数组顺序
        /// </summary>
        /// <param name="srcList"></param>
        /// <typeparam name="T"></typeparam>
        public static void RandomSelf<T>(this List<T> srcList)
        {
            for (int j = 0; j < srcList.Count; j++)
            {
                int x, y; T t;
                x = Random.Range(0, srcList.Count);
                do
                {
                    y = Random.Range(0, srcList.Count);
                } while (y == x);
 
                t = srcList[x];
                srcList[x] = srcList[y];
                srcList[y] = t;
            }
        }

        public static int GetItemIndex<T>(this List<T> srcList, T item) where T :IComparable
        {
            if (srcList.Count == 0 || srcList == null)
            {
                return -1;
            }

            int left = 0;
            int right = srcList.Count - 1;
            int mid = 0;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (srcList[mid].CompareTo(item) > 0)
                {
                    right = mid - 1;
                }
                else if (srcList[mid].CompareTo(item) == 0)
                {
                    return mid;
                }
                else if(srcList[mid].CompareTo(item) < 0)
                {
                    left = mid + 1;
                }
            }

            return -1;
        }
    }
}