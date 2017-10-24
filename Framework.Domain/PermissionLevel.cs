using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <summary>
    /// 权限等级划分
    /// </summary>
    [Flags]
    public enum PermissionLevel
    {
        /// <summary>
        /// 用户级，限定为阅读资源
        /// </summary>
        User=1,
        /// <summary>
        /// 特殊用户级，可以享受一定的特殊待遇和服务
        /// </summary>
        Vip=2,
        /// <summary>
        /// 后台系统管理员,对资源进行调度和调整
        /// </summary>
        Manager=4,
        /// <summary>
        /// 最高级别的用户管理员,用户的级别里最高
        /// </summary>
        Boss=6,
        /// <summary>
        /// 系统管理员，对系统一切资源进行维护，仅有一个
        /// </summary>
        Administrator=8
    }
}
