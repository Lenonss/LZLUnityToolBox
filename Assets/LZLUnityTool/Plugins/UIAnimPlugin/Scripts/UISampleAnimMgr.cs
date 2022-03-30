using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace LZLUnityTool.Plugins.UIAnimPlugin
{
    public class UISampleAnimMgr : MonoBehaviour
    {
        private enum AnimType
        {
            Move2D,Rotate,Size
        }
        private static void DoAnim(UISampleAnimUnit item, bool AddOrSubstruct, Action endCall,AnimType type)
        {
            if (item == null) return;
            
            //RectTransform获取
            RectTransform itemRectTransform = null;
            if ((itemRectTransform = item.GetComponent<RectTransform>()) == null)
            {
                Debug.LogError("UISampleAnimMgrMethod : " +
                               item.name.ToString()+" has not RectTramsform Component");
                return;
            }

            if (CheckTypeIsRight(type,item.UIAnimType))
            {
                //获得动画时间
                var duration = item._anchorV2.useContinueDuration ? item.duration : item._anchorV2.duration;
                //获得目标位置
                // var targetAnchorPos = itemRectTransform.anchoredPosition
                //                       + (AddOrSubstruct ? item._anchorV2.increment : -item._anchorV2.increment);
                var targetValue = GetTarValue(type, item, itemRectTransform, AddOrSubstruct);

                switch (type)
                {
                    case AnimType.Move2D:
                        itemRectTransform.DOAnchorPos(targetValue, duration,item._isSnaping);
                        break;
                    case AnimType.Rotate:
                        itemRectTransform.DOLocalRotate(targetValue, duration, item._rotateMode);
                        break;
                    case AnimType.Size:
                        itemRectTransform.DOScale(targetValue, duration);
                        break;
                }
                //开始移动
            }
            //调用函数
            endCall?.Invoke();
        }

        private static bool CheckTypeIsRight(AnimType type, UISampleAnimType sampleAnimType)
        {
            bool result = false;
            switch (type)
            {
                case AnimType.Move2D:
                    result = (sampleAnimType == UISampleAnimType.AnchorV2Move
                              || sampleAnimType == UISampleAnimType.AV2MaSV3
                              || sampleAnimType == UISampleAnimType.AV2MaLRV3
                              || sampleAnimType == UISampleAnimType.All);
                    break;
                case AnimType.Rotate:
                    result = (sampleAnimType == UISampleAnimType.LocalRotateV3 ||
                              sampleAnimType == UISampleAnimType.LRV3aSV3 ||
                              sampleAnimType == UISampleAnimType.AV2MaLRV3 ||
                              sampleAnimType == UISampleAnimType.All);
                    break;
                case AnimType.Size:
                    result = (sampleAnimType == UISampleAnimType.SizeV3 ||
                              sampleAnimType == UISampleAnimType.AV2MaSV3 ||
                              sampleAnimType == UISampleAnimType.LRV3aSV3 ||
                              sampleAnimType == UISampleAnimType.All);
                    break;
            }
            return result;
        }
        
        private static Vector3 GetTarValue(AnimType type,UISampleAnimUnit item,RectTransform itemRectTransform,bool addOrSub)
        {
            Vector3 result = Vector3.zero;
            switch (type)
            {
                case AnimType.Move2D:
                    result = itemRectTransform.anchoredPosition
                             + (addOrSub ? item._anchorV2.increment : -item._anchorV2.increment);
                    break;
                case AnimType.Rotate:
                    result =  itemRectTransform.localEulerAngles
                              + (addOrSub ? item._localRotateV3.increment : -item._localRotateV3.increment);
                    break;
                case AnimType.Size:
                    result =  itemRectTransform.localScale
                              + (addOrSub ? item._sizeV3.increment : -item._sizeV3.increment);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 基于二元锚点的UI移动
        /// </summary>
        /// <param name="animObjs">被移到的UI</param>
        /// <param name="AddOrSubstruct">true:+increment,false:-increment</param>
        /// <param name="endCall">结尾处调用</param>
        public static void DoAnchorV2Move(List<UISampleAnimUnit> animObjs,bool AddOrSubstruct,Action endCall = null)
        {
            if (animObjs.Count==0) return;
            foreach (var item in animObjs)
            {
                DoAnim(item,AddOrSubstruct,null,AnimType.Move2D);
            }
            //调用函数
            endCall?.Invoke();
        }
        /// <summary>
        /// 基于二元锚点的UI移动
        /// </summary>
        /// <param name="item">被移到的UI</param>
        /// <param name="AddOrSubstruct">true:+increment,false:-increment</param>
        /// <param name="endCall">结尾处调用</param>
        public static void DoAnchorV2Move(UISampleAnimUnit item,bool AddOrSubstruct,Action endCall = null)
        {
            DoAnim(item, AddOrSubstruct, endCall, AnimType.Move2D);
        }

        

        /// <summary>
        /// 基于本地欧拉角的UI旋转
        /// </summary>
        /// <param name="animObjs">被操作的UI</param>
        /// <param name="AddOrSubstract">true:+increment,false:-increment</param>
        /// <param name="endCall">结尾处调用</param>
        public static void DoLocalRotateV3(List<UISampleAnimUnit> animObjs, bool AddOrSubstract, Action endCall = null)
        {
            if (animObjs.Count==0) return;
            foreach (var item in animObjs)
            {
                DoAnim(item,AddOrSubstract,null,AnimType.Rotate);
            }
            //调用函数
            endCall?.Invoke();
        }
        /// <summary>
        /// 基于本地欧拉角的UI旋转
        /// </summary>
        /// <param name="item">被操作的UI</param>
        /// <param name="AddOrSubstract">true:+increment,false:-increment</param>
        /// <param name="endCall">结尾处调用</param>
        public static void DoLocalRotateV3(UISampleAnimUnit item, bool AddOrSubstract, Action endCall = null)
        {
            DoAnim(item,AddOrSubstract,endCall,AnimType.Rotate);
        }
        
        
        
        /// <summary>
        /// 基于Scale的缩放
        /// </summary>
        /// <param name="animObjs">被操作的UI</param>
        /// <param name="AddOrSubstract">true:+increment,false:-increment</param>
        /// <param name="endCall">结尾处调用</param>
        public static void DoSizeV3(List<UISampleAnimUnit> animObjs, bool AddOrSubstract, Action endCall = null)
        {
            if (animObjs.Count==0) return;
            foreach (var item in animObjs)
            {
                DoAnim(item,AddOrSubstract,null,AnimType.Size);
            }
            //调用函数
            endCall?.Invoke();
        }
        /// <summary>
        /// 基于Scale的缩放
        /// </summary>
        /// <param name="item">被操作的UI</param>
        /// <param name="AddOrSubstract">true:+increment,false:-increment</param>
        /// <param name="endCall">结尾处调用</param>
        public static void DoSizeV3(UISampleAnimUnit item, bool AddOrSubstract, Action endCall = null)
        {
            DoAnim(item,AddOrSubstract,endCall,AnimType.Size);
        }
    }

}
