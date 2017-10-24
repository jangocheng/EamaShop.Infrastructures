using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    /// <summary>
    /// 为所有的实体定义公共属性行为
    /// </summary>
    public abstract class BaseAggregate
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Status Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime ModifiedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual long Id { get; set; }
    }
}
