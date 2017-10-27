using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Framework.AspNetCore.Infrastructure
{
    public sealed class TokenOptions
    {
        public IList<Func<HttpContext, string>> AccessTokenProvider { get; } = new List<Func<HttpContext, string>>();
    }
}
