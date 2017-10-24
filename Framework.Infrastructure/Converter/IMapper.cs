using System;

namespace Framework.Infrastructure.Converter
{
    /// <summary>
    /// 定义将对象转换/映射为另一类型对象的行为
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="sourceType">源对象类型</param>
        /// <param name="desctinationType">目标对象类型</param>
        /// <returns></returns>
        object To(object source, Type sourceType, Type desctinationType);
    }
}
