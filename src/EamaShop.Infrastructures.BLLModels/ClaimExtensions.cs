using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using System.Linq;
using System.Extensions;

namespace EamaShop.Infrastructures.BLLModels
{
    /// <summary>
    /// Provides extension methods for <see cref="ClaimsPrincipal"/>
    /// </summary>
    public static class ClaimExtensions
    {
        /// <summary>
        /// Gets typed <see cref="EamaUser"/> from <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="principal">A <see cref="ClaimsPrincipal"/> create by eamauser</param>
        /// <returns>Typed user</returns>
        /// <exception cref="ArgumentNullException"><paramref name="principal"/></exception>
        public static EamaUser GetTypedUser(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return new EamaUser(principal);
        }
        /// <summary>
        /// Find first value name in current claims principal
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Claim.Value"/> cast to.</typeparam>
        /// <param name="principal">source principal stores user values</param>
        /// <param name="claimType">value of <see cref="Claim.Type"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">principal,claimType</exception>
        public static T FindFirstValue<T>(this ClaimsPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            if (claimType == null)
            {
                throw new ArgumentNullException(nameof(claimType));
            }

            if (principal.Claims == null || !principal.Claims.Any(x => x.Type == claimType))
            {
                return default(T);
            }

            var claim = principal.FindFirst(x => x.Type == claimType);

            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }

        /// <summary>
        /// Find first value from identity.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Claim.Value"/> cast to.</typeparam>
        /// <param name="identity">source identity stores user values</param>
        /// <param name="claimType">value of <see cref="Claim.Type"/></param>
        /// <returns></returns>
        public static T FindFirstValue<T>(this ClaimsIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (claimType == null)
            {
                throw new ArgumentNullException(nameof(claimType));
            }
            if (identity.Claims == null || !identity.Claims.Any())
            {
                return default(T);
            }

            var claim = identity.FindFirst(x => x.Type == claimType);
            return (T)Convert.ChangeType(claim.Value, typeof(T));
        }


    }
}
