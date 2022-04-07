using LZLUnityTool.Plugins.CommonPlugin.Interface;
using UnityEngine;

namespace LZLUnityTool.Plugins.CommonPlugin.ExtructFunction
{
    public static class CommonTool
    {
        /// <summary>
        /// 概率计算
        /// </summary>
        /// <param name="probability">百分比数值</param>
        /// <returns>是否命中</returns>
        public static bool Probability(float probability)
        {
            if (probability < 0) return false;
            return UnityEngine.Random.Range(100, 10001) * 0.01f <= probability;
        }
        
        /// <summary>
        /// 到目标透明值
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="duration"></param>
        /// <param name="fader"></param>
        /// <typeparam name="T"></typeparam>
        public static void FadeTo<T>(float alpha, float duration, IFadeAble<T> fader) where T : MonoBehaviour
        {
            if (fader.FadeCoroutine != null) 
                fader.MonoBehaviour.StopCoroutine(fader.FadeCoroutine);
            fader.FadeCoroutine = fader.MonoBehaviour.StartCoroutine(fader.Fade(alpha, duration));
        }
        
        /// <summary>
        /// 设置游戏体激活状态
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="value"></param>
        public static void SetActive(GameObject gameObject, bool value)
        {
            if (!gameObject) return;
            if (gameObject.activeSelf != value) gameObject.SetActive(value);
        }
        public static void SetActive(Component component, bool value)
        {
            if (!component) return;
            SetActive(component.gameObject, value);
        }
        
        #region Component
        
        /// <summary>
        /// 检查T组件是否挂载载gameObject上
        /// </summary>
        /// <param name="gameObject">目标游戏体</param>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public  static bool CheckComponentIsExist<T>(GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }
        
        /// <summary>
        /// 检查T组件是否挂载在gameObject上，会给para赋值
        /// </summary>
        /// <param name="gameObject">目标游戏体</param>
        /// <param name="para">存储目标组件的对象</param>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public static bool CheckComponentIsExist<T>(GameObject gameObject,ref T para)
        {
            if (para != null)
            {
                return true;
            }

            para = gameObject.GetComponent<T>();
            return para != null;
        }

        #endregion
    }
}