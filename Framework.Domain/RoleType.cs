using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Flags]
    public enum RoleType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 2,
        /// <summary>
        /// 普通用户
        /// </summary>
        User=8,
    }
}
