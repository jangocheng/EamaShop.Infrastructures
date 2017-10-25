﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 值类型的扩展
    /// </summary>
    public static class ValueTypeExtensions
    {
        /// <summary>
        /// 是否是默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }
    }
}