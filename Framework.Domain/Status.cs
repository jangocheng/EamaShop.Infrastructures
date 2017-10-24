using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <summary>
    /// 数据库记录的当前状态
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 可用状态，表示是一条有效数据
        /// </summary>
        Ok,
        /// <summary>
        /// 不可用状态，表示该数据已经被逻辑删除
        /// </summary>
        Delete
    }
}
