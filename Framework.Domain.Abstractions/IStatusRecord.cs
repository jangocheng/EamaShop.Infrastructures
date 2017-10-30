using System;

namespace Framework.Domain.Abstractions
{
    /// <summary>
    /// 表示是具有状态的数据记录
    /// </summary>
    public interface IStatusRecord
    {
        /// <summary>
        /// 记录的状态
        /// </summary>
        RecordStatus Status { get; set; }
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
