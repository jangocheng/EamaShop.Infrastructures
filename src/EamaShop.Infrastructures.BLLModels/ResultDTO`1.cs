using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// Wrapper class for DTO returns in Mvc Restful-API Controller.
    /// </summary>
    /// <typeparam name="TDTO">The type of DTO need Wrappered</typeparam>
    public class ResultDTO<TDTO> : ResultDTO
    {
        /// <summary>
        /// Initialize a new DTO Result with success response data.
        /// </summary>
        /// <param name="data"></param>
        public ResultDTO(TDTO data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the data of API returns.
        /// </summary>
        public TDTO Data { get; }
    }
    public class ResultDTO
    {
        /// <summary>
        /// Get description for response.
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// Initialize a new <see cref="ResultDTO"/> instance.
        /// </summary>
        /// <param name="message"></param>
        protected ResultDTO(string message = "SUCCESS")
        {
            Message = message;
        }
        /// <summary>
        /// New a result dto by given message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResultDTO New(string message)
        {
            return new ResultDTO(message);
        }
        /// <summary>
        /// New a result dto by using default 'SUCCESS' message.
        /// </summary>
        /// <returns></returns>
        public static ResultDTO New()
        {
            return new ResultDTO();
        }
        /// <summary>
        /// 成功，包含数据
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResultDTO Ok<TDTO>(TDTO data)
        {
            return new ResultDTO<TDTO>(data);
        }
    }
}
