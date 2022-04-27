using System;
using System.Collections;
using System.Collections.Generic;

namespace LZLUnityTool.Plugins.MathPlugin
{
    public struct ZFloat64
    {
        private long scaledValue;
        public long ScaledValue
        {
            get => scaledValue;
            set => scaledValue = value;
        }
        
        
        //移位计数
        private const int BIT_MOVE_COUNT = 10;
        private const long MULTIPLIER_FACTOR = 1 << BIT_MOVE_COUNT;//2^BIT_MOVE_COUNT
        
        //常用单位数值

        
        #region 构造函数
        //内部使用，供已经缩放完成的数据使用
        public ZFloat64(long scaledValue)
        {
            this.scaledValue = scaledValue;
        }

        public ZFloat64(int val)
        {
            scaledValue = val * MULTIPLIER_FACTOR;
        }

        public ZFloat64(float val)
        {
            scaledValue = (long)Math.Round(val * MULTIPLIER_FACTOR);
        }

        //float 类型会损失精度，必须显式转换
        public static explicit operator ZFloat64(float f)
        {
            return new ZFloat64((long)Math.Round(f * MULTIPLIER_FACTOR));
        }

        
        #endregion
        
        //===========隐式转换
        //int类型不损失精度，可以隐式转换
        public static implicit operator ZFloat64(int src)
        {
            return new ZFloat64(src);
        }
    }
}

