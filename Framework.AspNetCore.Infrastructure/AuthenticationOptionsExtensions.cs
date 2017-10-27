using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace Framework.AspNetCore.Infrastructure
{
    public static class AuthenticationOptionsExtensions
    {
        private const string Name = "OpenId";
        /// <summary>
        /// Add OpenId Authentication
        /// </summary>
        /// <param name="options"></param>
        /// <param name="asDefault"></param>
        /// <returns></returns>
        public static AuthenticationOptions AddOpenId(this AuthenticationOptions options, bool asDefault = true)
        {
            
            options.AddScheme<TokenAuthenticationHandler>(Name, "授权");
            if (asDefault)
            {
                options.DefaultAuthenticateScheme = Name;
                options.DefaultChallengeScheme = Name;
                options.DefaultForbidScheme = Name;
                options.DefaultScheme = Name;
                options.DefaultSignInScheme = Name;
                options.DefaultSignOutScheme = Name;
            }
            return options;
        }
    }
}
