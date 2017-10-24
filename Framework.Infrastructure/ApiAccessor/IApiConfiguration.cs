using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <summary>
    /// 表示访问第三方服务器的客户端信息上下文
    /// </summary>
    public interface IApiConfiguration
    {
        /// <summary>
        /// 获取平台的中文名称信息
        /// </summary>
        string Name { get; }
    }
}
