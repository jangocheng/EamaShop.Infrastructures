using System;
using System.Collections.Generic;
using System.Text;

    
namespace Framework.Infrastructure.Data
{
    /// <summary>
    /// 表示时间区间的所有条件
    /// </summary>
    public sealed class DateTimeCondition:IntervalCondition<DateTime>
    {

        /// <inheritdoc />
        /// <summary>
        /// 初始化 <see cref="T:Framework.Infrastructure.DateTimeCondition" /> 类型的新实例,并使用区间 1970-01-01 00:00:00 To now
        /// </summary>
        public DateTimeCondition() : this(new DateTime(1970, 1, 1), DateTime.Now)
        {

        }

        /// <summary>
        /// 使用给定的时间区间初始化 <see cref="DateTimeCondition"/> 类型的新实例
        /// </summary>
        public DateTimeCondition(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        /// <summary>
        /// 从指定的时间开始，默认从1970年1月1日开始
        /// </summary>
        public override DateTime Start { get; set; }

        /// <summary>
        /// 截至到指定的时间 默认截至到当前
        /// </summary>
        public override DateTime End { get; set; }
    }
}
