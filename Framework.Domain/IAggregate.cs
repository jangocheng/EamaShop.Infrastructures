using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <summary>
    /// 领域聚合根
    /// </summary>
    public interface IAggregate
    {
        /// <summary>
        /// 主键
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// 记录的状态
        /// </summary>
        Status Status { get; set; }
        /// <summary>
        /// 数据的创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 上次修改该数据的时间
        /// </summary>
        DateTime ModifiedTime { get; set; }
    }
}
