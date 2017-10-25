using System;

namespace Framework.Domain.Abstractions
{
    /// <summary>
    /// 定义领域对象的聚合属性
    /// </summary>
    public interface IAggregate
    {

        /// <summary>
        /// 主键
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// 记录的状态
        /// </summary>
        RecordStatus Status { get; set; }
        /// <summary>
        /// 数据的创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 上次修改该数据的时间
        /// </summary>
        DateTime ModifiedTime { get; set; }

    }
}
