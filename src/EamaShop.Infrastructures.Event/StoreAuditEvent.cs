using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.Events
{
    /// <summary>
    /// 店铺创建的审核事件
    /// </summary>
    public class StoreAuditEvent : IEventMetadata
    {
        /// <summary>
        /// 是否同意门店的创建申请
        /// </summary>
        public bool Agree { get; set; }
        /// <summary>
        /// 创建门店的申请单据的单据id
        /// </summary>
        public long AuditId { get; set; }
        /// <summary>
        /// 申请创建门店的用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 审核员的Id
        /// </summary>
        public long AuditorId { get; set; }
        /// <summary>
        /// 创建的门店Id
        /// </summary>
        public long StoreId { get; set; }
        /// <summary>
        /// 审核的时间
        /// </summary>
        public DateTime AuditTime { get; set; }
    }
}
