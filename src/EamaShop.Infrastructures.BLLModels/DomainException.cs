using System;

namespace System
{
    /// <summary>
    /// 领域业务异常
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : base(message)
        {
        }
    }
}
