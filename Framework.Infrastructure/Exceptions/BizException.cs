using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// 表示业务异常,只有在业务出现无法继续进行时,才引发该异常,不应该在代码中直接使用该类型，使用<see cref="Check.BizShortCircuit(string)"/>
    /// </summary>
    public class BizException : Exception
    {

        /// <inheritdoc />
        /// <summary>
        /// 初始化 <see cref="T:Framework.Infrastructure.Exceptions.BizException" /> 类型的新实例
        /// </summary>
        public BizException(string message)
        {
            Message = message;
        }
        /// <summary>
        /// 展示的用户文案
        /// </summary>
        public override string Message { get; }
    }
}
