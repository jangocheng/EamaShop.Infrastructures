using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Entity
{
    /// <summary>
    /// 表示数据记录的状态
    /// </summary>
    public enum RecordStatus
    {
        /// <summary>
        /// 正常的记录
        /// </summary>
        Ok,
        /// <summary>
        /// 已经被删除的记录
        /// </summary>
        Delete
    }
}
