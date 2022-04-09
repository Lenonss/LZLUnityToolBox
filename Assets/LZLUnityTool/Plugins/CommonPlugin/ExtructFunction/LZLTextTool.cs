using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LZLUnityTool.Plugins.CommonPlugin.ExtructFunction
{
    public static class LZLTextTool
    {
        /// <summary>
        /// 颜色震动
        /// </summary>
        /// <param name="self">需要颜色震动的文本组件</param>
        /// <param name="oriColor">初始颜色</param>
        /// <param name="tarColor">震动目标颜色</param>
        /// <param name="shakeDuration">震动持续时间</param>
        /// <param name="ignoreTimeScale">是否忽略时间尺寸的影响</param>
        /// <returns></returns>
        public static bool ColorShake(this Text self, Color oriColor, Color tarColor,float shakeDuration,bool ignoreTimeScale = false)
        {
            if (self.color == tarColor)
            {
                return false;
            }

            self.color = tarColor;
            DOVirtual.DelayedCall(shakeDuration, () => self.color = oriColor, ignoreTimeScale);
            return true;
        }
        
    }
}