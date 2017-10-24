using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <inheritdoc cref="IAggregate" />
    /// <summary>
    /// 验证码
    /// </summary>
    public class VerifyCode:BaseAggregate,IAggregate
    {
        /// <summary>
        /// 验证码内容
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 验证码的验证目标
        /// </summary>
        public string Target { get; set; }
    }
}
