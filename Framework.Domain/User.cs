using System;
using System.Collections.Generic;

namespace Framework.Domain
{
    /// <inheritdoc cref="IAggregate" />
    /// <summary>
    /// 用户对象
    /// </summary>
    public class User : BaseAggregate, IAggregate
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public RoleType Role { get; set; }
    }
}