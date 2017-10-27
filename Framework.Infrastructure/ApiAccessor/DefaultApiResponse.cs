using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Framework.Infrastructure.ApiAccessor
{
    /// <inheritdoc />
    public class DefaultApiResponse<TResult> : ApiResponse<TResult>
    {
        /// <inheritdoc />
        public DefaultApiResponse(HttpStatusCode code, IDictionary<string, string> headers, TResult result) : base(code, headers)
        {
            Result = result;
        }

        /// <inheritdoc />
        public override TResult Result { get; }
    }
}
