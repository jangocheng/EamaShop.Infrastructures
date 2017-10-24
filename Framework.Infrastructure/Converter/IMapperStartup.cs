using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Framework.Infrastructure.Converter
{
    /// <summary>
    /// automapper注册支持
    /// </summary>
    public interface IMapperStartup
    {
        /// <summary>
        /// 初始化automapper的相关数据
        /// </summary>
        /// <param name="config"></param>
        void Initialize(IMapperConfigurationExpression config);
    }
}
