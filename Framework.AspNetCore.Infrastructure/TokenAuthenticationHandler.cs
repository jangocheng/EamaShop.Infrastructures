using System;
using System.Collections;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Framework.AspNetCore.Infrastructure
{
    /// <inheritdoc />
    /// <summary>
    /// 基于Token的身份认证
    /// </summary>
    public class TokenAuthenticationHandler : IAuthenticationSignInHandler
    {
        public TokenAuthenticationHandler(IOptions<TokenOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _options = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
        }

        private readonly TokenOptions _options;
        private AuthenticationScheme _schame;
        private HttpContext _context;
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _schame = scheme;
            _context = context;
            return Task.CompletedTask;
        }

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            string token = null;

            foreach (var p in _options.AccessTokenProvider)
            {
                token = p?.Invoke(_context);
            }

            if (token == null)
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.NoResult());
            }

            if (token.Length != 32)
            {
                return Task.FromResult(AuthenticateResult.Fail("invalid access token"));
            }

            var name = new Claim(ClaimTypes.Name, "username", ClaimValueTypes.String);
            var role = new Claim(ClaimTypes.Role, "admin", ClaimValueTypes.String);
            var id = new Claim("Id", "1", ClaimValueTypes.Integer64);
            var phone = new Claim(ClaimTypes.MobilePhone, "15056669295", ClaimValueTypes.String);
            var createTime = new Claim("createtime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ClaimValueTypes.DateTime);

            var identity = new ClaimsIdentity(new[] { name, role, id }, _schame.Name, ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.AddDays(1),
                RedirectUri = "https://www.baidu.com"
            };

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, properties, _schame.Name)));
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {

            _context.Response.Redirect(properties?.RedirectUri);
            return _context.Response.WriteAsync("you have to login first");
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            return _context.Response.WriteAsync("you have no access");
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }
    }
}
