using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Framework.AspNetCore.Infrastructure
{
    public static class ClaimExtensions
    {
        public static long UserId(this ClaimsPrincipal principal)
        {
            var id = principal.Claims.FirstOrDefault(x => x.Type == "Id" && x.ValueType == ClaimValueTypes.Integer64)?.Value;

            if (id == null)
            {
                throw new InvalidOperationException("CurrentUser has no id");
            }

            if (long.TryParse(id, out var r))
            {
                return r;
            }

            throw new InvalidOperationException("CurrentUser has no id");
        }

        public static bool IsInRole(this ClaimsPrincipal principal, Enum e)
        {
            return principal.IsInRole(e.ToString());
        }

        public static DateTime CreateTime(this ClaimsPrincipal principal)
        {
            var datetime =
                principal.Claims.FirstOrDefault(x => x.Type == "createTime" && x.ValueType == ClaimValueTypes.DateTime)?.Value;

            if (datetime == null)
            {
                return DateTime.MinValue;
            }

            if (DateTime.TryParse(datetime, out var d))
            {
                return d;
            }

            return DateTime.MinValue;

        }
    }
}
