using Framework.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Infrastructure
{
    /// <summary>
    /// Ip地址字符串
    /// </summary>
    public sealed class IpAddressString
    {
        private static HttpClient _client = new HttpClient();
        private const string Pattern = "^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\."
            + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."
            + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."
            + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$";
        public static string ErrorMsg { get; set; } = "无效的Ip地址，请输入正确的Ip地址";
        public static Func<string, bool> Rule { get; set; } = (i) => Regex.IsMatch(i, Pattern);
        public string IpAddress { get; }
        /// <summary>
        /// 初始化一个Ip地址
        /// </summary>
        /// <param name="ipAddress"></param>
        public IpAddressString(string ipAddress)
        {
            if (!Rule(ipAddress))
            {
                throw new ArgumentException(ErrorMsg);
            }
            IpAddress = ipAddress;
        }
        #region Helper
        private IpInfoData AddressInfo = null;
        /// <summary>
        /// 获取当前Ip地址的信息 获取不到返回null
        /// </summary>
        /// <returns>ip地址的信息</returns>
        public IpInfoData GetAddressInfo()
        {
            if (AddressInfo != null)
            {
                return AddressInfo;
            }
            var json = _client.GetAsync($"http://ip.taobao.com/service/getIpInfo.php?ip={IpAddress}").Result.Content.ReadAsStringAsync().Result;

            return json.DeserializeJson<IpInforamtion>()?.Data;
        }
        private class IpInforamtion
        {
            public int Code { get; set; }

            public IpInfoData Data { get; set; }
        }
        /// <summary>
        /// 地址信息
        /// </summary>
        public class IpInfoData
        {
            /// <summary>
            /// 国家
            /// </summary>
            public string Country { get; set; }

            /// <summary>
            /// 地区 Eg.华东 华北
            /// </summary>
            public string Area { get; set; }
            /// <summary>
            /// 区域省份  浙江省
            /// </summary>

            public string Region { get; set; }
            /// <summary>
            /// 城市 杭州
            /// </summary>

            public string City { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string County { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Isp { get; set; }


        }
        #endregion
        /// <summary>
        /// 隐式转换为<see cref="string"/>
        /// </summary>
        /// <param name="ipAddress"></param>
        public static implicit operator string(IpAddressString ipAddress)
        {
            return ipAddress?.IpAddress;
        }
        /// <summary>
        /// 隐式转换为 <see cref="IpAddressString"/>
        /// </summary>
        /// <param name="ipAddress"></param>
        public static implicit operator IpAddressString(string ipAddress)
        {
            return new IpAddressString(ipAddress);
        }
        /// <summary>
        /// 比较俩个ip地址是否相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is IpAddressString un && IpAddress.Equals(un)) || (obj is string sv && IpAddress.Equals(sv));
        }
        /// <summary>
        /// 获取ip的hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return IpAddress.GetHashCode();
        }
        /// <summary>
        /// 获取ip地址
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return IpAddress;
        }
    }
}
