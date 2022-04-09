using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace LZLUnityTool.Plugins.UIPlugin.NewerGuid.Scripts
{
    public class NewerGuideMask : MonoBehaviour,ICanvasRaycastFilter
    {
            [Header("变化速度配置")]
            public bool ignoreTimeScale = false;
        public AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [Range(0.1f, 500)] public float unitChangeScale = 10;
        [Range(0, 1)] public float unitDurationScale = 0.1f;


        [Header("开始时自动使用")] 
        public bool playOnStart = true;

        [Header("自定义画布")] 
        public bool customCanvas;
        [InfoBox("自定义选择的画布", InfoMessageType.None), ShowIf("customCanvas", true)]
        public Canvas targetCanvas;
        
        public List<RectTransform> targets;
        public bool isCircle = true;

        [Range(1,100)]
        public float rectangleDis = 10;

        /// <summary>
        /// 区域范围缓存
        /// </summary>
        private Vector3[] _corners = new Vector3[4];
        
        /// <summary>
        /// 遮罩材质
        /// </summary>
        private Material _material;
        
        private Canvas _canvas;
        private Coroutine _changeCoro;
        private int _currentIndex;
        private RectTransform _clickArea;

        public Action OnChangingAnimStart;
        public Action OnChangingAnimOver;

        //===============生命周期函数
        private void Awake()
        {
            _currentIndex = 0;
            _canvas = targetCanvas ? targetCanvas : GameObject.Find("Canvas").GetComponent<Canvas>();
            _material = GetComponent<Image>().material;
            
            
        }
        private void Start()
        {
            if (playOnStart)
                //获取画布
                StartCoroutine(DelayToCall(Time.deltaTime, ChangeTarget));
        }


        //===============开始功能接口
        public void ChangeTarget()
        {
            if (_currentIndex >= targets.Count)
                _currentIndex = 0;
            ChangeTarget(targets[_currentIndex]);
            _currentIndex++;
        }
        public void ChangeTarget(RectTransform target)
        {
            if (target == null)
                return;
            
            //参数赋值
            _clickArea = target;
            Vector2 screenAdaptePara = new Vector2(Screen.width / 2, Screen.height / 2);//适配参数
            
            //计算高亮显示区域的圆心
            _clickArea.GetWorldCorners(_corners);
            float x = _corners[0].x + ((_corners[3].x - _corners[0].x) / 2f);
            float y = _corners[0].y + ((_corners[1].y - _corners[0].y) / 2f);
            Vector3 centerWorld = new Vector3(x, y, 0);
            
            //设置遮罩材料中的圆心变量
            Vector4 centerMat = new Vector4(centerWorld.x - screenAdaptePara.x, 
                centerWorld.y - screenAdaptePara.y, 0, 0);
            _material.SetVector("_Center", centerMat);

            if (isCircle)
            {
                //计算最终高亮显示区域的半径
                var targetRadius = Vector2.Distance(WorldToCanvasPos(_canvas, _corners[0]),
                    WorldToCanvasPos(_canvas, _corners[2])) / 2f;

                float maxRadius = GetMaxRadius(centerWorld);
            
                _material.SetFloat("_EdgeSlider", maxRadius);
                StartPlayChangeCoro(maxRadius, targetRadius);
            }
            else
            {
                Vector3[] cavasCorners = new Vector3[4];
                Vector3[] centerCorners = new Vector3[4];
                Vector4 Lengths = Vector4.zero;//上下左右
                Vector4 rectangleDis = new Vector4(this.rectangleDis, this.rectangleDis, this.rectangleDis,
                    this.rectangleDis);
                _canvas.GetComponent<RectTransform>().GetWorldCorners(cavasCorners);
                _clickArea.GetWorldCorners(centerCorners);
                Lengths.x = Mathf.Abs(cavasCorners[1].y - centerCorners[1].y);//距离上边界距离
                Lengths.y = Mathf.Abs(cavasCorners[0].y - centerCorners[0].y);//距离下边界距离
                Lengths.z = Mathf.Abs(cavasCorners[1].x - centerCorners[1].x);//距离左边界距离
                Lengths.w = Mathf.Abs(cavasCorners[2].x - centerCorners[2].x);//距离右边界距离
                
                Vector4 minLengths = new Vector4(centerCorners[1].y-centerWorld.y,centerWorld.y - centerCorners[0].y,
                centerWorld.x - centerCorners[0].x,centerCorners[2].x-centerWorld.x);
                Vector4 maxLengths = minLengths + Lengths;
                minLengths += rectangleDis;
                StartCoroutine(PlayChange(maxLengths, minLengths));
            }
        }
        
        
        //===============镂空实现
        IEnumerator PlayChange(float currentRidus, float targetRidus)
        {
            OnChangingAnimStart?.Invoke();

            _material.SetFloat("_Type", isCircle ? 0 : 1);
            
            float unitDuration = 0;
            float unitDelta = 0;
            float delta = targetRidus - currentRidus;
            float unitChangeValue = delta == 0 ? 0 : delta / Mathf.Abs(delta);
            unitChangeValue *= unitChangeScale;

            bool bigSmaleState = targetRidus >= currentRidus ? true : false;
            while (true)
            {
                currentRidus += unitChangeValue;
                _material.SetFloat("_EdgeSlider", currentRidus);
                
                unitDelta = Mathf.Abs(targetRidus - currentRidus);
                unitDuration = Mathf.Clamp01(1 - animationCurve.Evaluate(unitDelta / Mathf.Abs(delta)));

                if (ignoreTimeScale)
                {
                    yield return new WaitForSecondsRealtime(unitDuration * unitDurationScale);
                }
                else
                {
                    yield return new WaitForSeconds(unitDuration * unitDurationScale);
                }

                if ((bigSmaleState && currentRidus >= targetRidus)
                    || (!bigSmaleState && currentRidus <= targetRidus))
                {
                    OnChangingAnimOver?.Invoke();
                    break;
                }
            }

            StopPlayChangeCoro();
        }

        IEnumerator PlayChange(Vector4 maxLengths,Vector4 minLengths)
        {
            OnChangingAnimStart?.Invoke();

            
            _material.SetFloat("_Type", isCircle ? 0 : 1);
            float percent = 0;
            float unitDuration = 0;
            Vector4 curLengths = maxLengths;
            while (true)
            {
                curLengths = Vector4.Lerp(maxLengths,minLengths,percent);
                _material.SetVector("_Lengths",curLengths);
                unitDuration = Mathf.Clamp01(1 - animationCurve.Evaluate(percent / 1f));
                if (ignoreTimeScale)
                {
                    yield return new WaitForSecondsRealtime(unitDuration * unitDurationScale);
                }
                else
                {
                    yield return new WaitForSeconds(unitDuration * unitDurationScale);
                }
                if (percent>=1)
                {
                    OnChangingAnimOver?.Invoke();
                    break;
                }
                percent += 0.01f;
            }
            StopPlayChangeCoro();
        }

        private void StartPlayChangeCoro(float currentRidus, float targetRidus)
        {
            StopPlayChangeCoro();
            _changeCoro = StartCoroutine(PlayChange(currentRidus, targetRidus));
        }
        private void StopPlayChangeCoro()
        {
            if (_changeCoro != null)
            {
                StopCoroutine(_changeCoro);
                _changeCoro = null;
            }
        }
        
        
        //===============工具函数
        IEnumerator DelayToCall(float delayDuration, Action call)
        {
            yield return new WaitForSecondsRealtime(delayDuration);
            call?.Invoke();
        }
        
        /// <summary>
        /// 获得镂空区域最大半径
        /// </summary>
        /// <param name="centerWorld">目标镂空区的坐标</param>
        /// <returns></returns>
        private float GetMaxRadius(Vector3 centerWorld)
        {
            float result = 0;
            //计算当前高亮显示区域的半径
            RectTransform canRectTransform = _canvas.transform as RectTransform;
            if (canRectTransform != null)
            {
                canRectTransform.GetWorldCorners(_corners);//获取画布区域的四个顶点
                foreach (Vector3 corner in _corners)//将画布顶点距离高亮区域中心最远的距离作为当前高亮区域半径的初始值
                {
                    result = Mathf.Max(Vector3.Distance(corner, centerWorld), result);
                }
            }

            return result;
        }

        /// <summary>
        /// 世界坐标向画布坐标转换
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="world">世界坐标</param>
        /// <returns>返回画布上的二维坐标</returns>
        private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                world, null, out position);
            return position;
        }

        #region 事件阻挡
        //没有目标则捕捉事件渗透
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (_clickArea == null)
            {
                return true;
            }
            //在目标范围内做事件渗透,遮挡的阻隔是否在该区域有效
            return !RectTransformUtility.RectangleContainsScreenPoint(_clickArea,
                sp, eventCamera);
        }

        #endregion
    }
}