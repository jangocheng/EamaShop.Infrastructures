using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EamaShop.Infrastructures.BLLModels
{
    /// <summary>
    /// Provides a typed user for accessing <see cref="ClaimsPrincipal"/>.
    /// 可靠字段：即每个用户被分配后无法更改的字段。
    /// </summary>
    public class EamaUser
    {
        private static string _authenticationType = "JwtBearer";
        /// <summary>
        /// Sets custome authentication type if not set custome before.
        /// </summary>
        /// <param name="authenticationType"></param>
        public static void SetAuthenticationTypeOnFirstTime(string authenticationType)
        {
            if (_authenticationType == "JwtBearer")
            {
                _authenticationType = authenticationType;
            }
        }
        private readonly ClaimsPrincipal _principal;
        /// <summary>
        /// Initialized a new <see cref="EamaUser"/> instance by using given <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="principal"></param>
        public EamaUser(ClaimsPrincipal principal)
        {
            _principal = principal ?? throw new ArgumentNullException(nameof(principal));
        }
        /// <summary>
        /// Initialize a new female <see cref="EamaUser"/> instance who login at now.
        /// </summary>
        /// <param name="id">User Identifier</param>
        /// <param name="accountName">User Account Name/AccountNumber</param>
        /// <param name="roles">User Roles Arrary</param>
        public EamaUser(long id, string accountName, string[] roles) : this(id, accountName, roles, true)
        {
        }
        /// <summary>
        /// Initialize a new <see cref="EamaUser"/> instance who login at now.
        /// </summary>
        /// <param name="id">User Identifier</param>
        /// <param name="accountName">User Account Name/AccountNumber</param>
        /// <param name="roles">User Roles Arrary</param>
        /// <param name="isFemale">Is Female</param>
        public EamaUser(long id, string accountName, string[] roles, bool isFemale)
            : this(id, accountName, roles, isFemale, DateTime.Now)
        {
        }
        /// <summary>
        /// Initialize a new <see cref="EamaUser"/> instance .
        /// </summary>
        /// <param name="id">User Identifier</param>
        /// <param name="accountName">User Account Name/AccountNumber</param>
        /// <param name="roles">User Roles Arrary</param>
        /// <param name="isFemale">Is Female</param>
        /// <param name="loginTime">login time</param>
        public EamaUser(long id, string accountName, string[] roles, bool isFemale, DateTime loginTime)
        {
            if (id < 1) throw new ArgumentException("id must greator than zero", nameof(id));
            if (accountName == null)
            {
                throw new ArgumentNullException(nameof(accountName));
            }

            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            var identities = new List<ClaimsIdentity>();
            foreach (var role in roles)
            {
                var claims = new[]
                {
                    NewClaim(nameof(Id),id),
                    NewClaim(nameof(AccountName),accountName),
                    NewClaim(nameof(IsFemale),isFemale),
                    NewClaim(nameof(LoginTime),loginTime),
                    NewClaim(nameof(Roles),String.Join(";",roles)),
                    NewClaim(ClaimsIdentity.DefaultRoleClaimType,role)
                };

                var identity = new ClaimsIdentity(claims, _authenticationType, nameof(AccountName), ClaimsIdentity.DefaultRoleClaimType);

                identities.Add(identity);
            }
            _principal = new ClaimsPrincipal(identities);
        }

        private static Claim NewClaim<T>(string fieldName, T value)
        {
            return new Claim(fieldName, value.ToString(), typeof(T).Name, ClaimsIdentity.DefaultIssuer);
        }
        /// <summary>
        /// 获取用户的唯一身份标识 对应了 claims的 Id字段值，该字段为可靠字段
        /// </summary>
        [Required]
        [Range(1, long.MaxValue)]
        public long Id => _principal.FindFirstValue<long>(nameof(Id));
        /// <summary>
        /// 获取用户注册时的用户名 ，该字段为可靠字段
        /// </summary>
        [Required]
        public string AccountName => _principal.FindFirstValue<string>(nameof(AccountName));
        /// <summary>
        /// 获取用户的身份角色 
        /// </summary>
        /// <remarks>
        /// 对应了<see cref="ClaimsPrincipal"/>中claimType 为 Roles 的字段，
        /// claimType=‘Roles’的字段值为 由 ';' 连接的字符串.
        /// 该字段可能不可靠，应该业务上告知用户要重新登陆
        /// </remarks>
        [Required]
        [MinLength(1)]
        public string[] Roles => _principal.FindFirstValue<string>(nameof(Roles))?.Split(';')
            ?? new string[0];
        /// <summary>
        /// 用户性别是否是女性 <see langword="true"/> 为女 <see langword="false"/> 为男。
        /// </summary>
        /// <remarks>
        /// 该字段可能不可靠
        /// </remarks>
        [DefaultValue(true)]
        public bool IsFemale => _principal.FindFirstValue<bool>(nameof(IsFemale));
        /// <summary>
        /// 获取当前用户登陆的时间
        /// </summary>
        /// <remarks>
        /// 可靠字段
        /// </remarks>
        public DateTime LoginTime => _principal.FindFirstValue<DateTime>(nameof(LoginTime));
        /// <summary>
        /// Transform current instance to <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipal()
        {
            return _principal;
        }

    }
}
