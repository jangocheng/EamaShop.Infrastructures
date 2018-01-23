using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EamaShop.Infrastructures.BLLModels
{
    /// <summary>
    /// 表示强类型的用户对象
    /// </summary>
    public class EamaShopUser
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; }

        public string AccoutName { get; }
    }
}
