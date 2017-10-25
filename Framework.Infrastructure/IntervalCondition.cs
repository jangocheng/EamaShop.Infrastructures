using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure
{
    /// <summary>
    /// 表示区间的搜索条件
    /// </summary>
    public class IntervalCondition<T>
    {
        /// <summary>
        /// 从该区间开始
        /// </summary>
        public virtual T Start { get; set; }
        /// <summary>
        /// 从该区间结束
        /// </summary>
        public virtual T End { get; set; }
    }
}
