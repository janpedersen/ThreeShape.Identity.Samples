using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OAuthWpfApp.Login
{
    public class IdentityHandler
    {
        private OidcClient _client;
        private LoginResult _loginResult;
        private readonly bool _loginWithEmbeddedBrowser;


        public IdentityHandler(bool loginWithEmbeddedBrowser)
        {
            this._loginWithEmbeddedBrowser = loginWithEmbeddedBrowser;
        }

        public void InitLogin()
        {

            _client = new OidcClient(new OidcClientOptions
            {
                Authority = "https://staging-identity.3shape.com",
                ClientId = "OAuthWpfAppSample",
                Scope = "openid data.users.read_only data.companies.read_only",
                RedirectUri = "http://127.0.0.1:9555/",
                Browser = _loginWithEmbeddedBrowser
                    ? (IBrowser)new WpfEmbeddedBrowser()
                    : (IBrowser)new SystemWebBrowser(),
            });
        }

        public async Task DoLoginAsync()
        {
            _loginResult = await _client.LoginAsync();
        }

        public bool IsLoginSuccessful => !_loginResult.IsError;

        public string LoginError => _loginResult.Error;

        public UserClaims LoggedInUser => new UserClaims
        {
            Id = GetClaims("sub", _loginResult.User.Claims),

            Email = _loginResult.User.Identity.Name,

            FullName = GetClaims("firstName", _loginResult.User.Claims) + " " + GetClaims("lastName", _loginResult.User.Claims),

            CompanyId = GetClaims("CompanyId", _loginResult.User.Claims),

            Roles = GetClaims("role", _loginResult.User.Claims),

            Ip = GetClaims("user_ip", _loginResult.User.Claims),

            Token = _loginResult.AccessToken,

            TokenExpiresAt = _loginResult.AccessTokenExpiration,
        };

        private string GetClaims(string roleName, IEnumerable<Claim> claims)
        {
            var claimList = claims.Where(x => x.Type == roleName).Select(x => x.Value).Distinct();
            return string.Join(" ", claimList);
        }

    }
}
