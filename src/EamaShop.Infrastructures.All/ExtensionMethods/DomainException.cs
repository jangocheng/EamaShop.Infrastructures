using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System
{
    /// <summary>
    /// 表示领域异常 一般用于表示业务异常
    /// </summary>
    [DebuggerStepThrough]
    public class DomainException : Exception
    {
        /// <summary>
        /// 初始化领域异常的新实例
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message):base(message)
        {

        }
    }
}
