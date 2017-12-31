using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// Wrapper class for DTO returns in Mvc Restful-API Controller.
    /// </summary>
    /// <typeparam name="TDTO">The type of DTO need Wrappered</typeparam>
    public struct ResultDTO<TDTO>
    {
        public ResultDTO(TDTO data) : this()
        {
            Data = data;
        }

        public ResultDTO(TDTO data, string message) : this()
        {
            Data = data;
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        /// <summary>
        /// Gets the data of API returns.
        /// </summary>
        public TDTO Data { get; }
        /// <summary>
        /// Gets the message description of API returns.
        /// </summary>
        public string Message { get; }
    }
}
