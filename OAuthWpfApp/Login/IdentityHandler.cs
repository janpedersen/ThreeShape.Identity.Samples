using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
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
            _loginWithEmbeddedBrowser = loginWithEmbeddedBrowser;
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

        public DateTime AccessTokenExpiresAt => _loginResult.AccessTokenExpiration;
        public string AccessToken => _loginResult.AccessToken;
        public string RefreshToken => _loginResult.RefreshToken;

        public IEnumerable<Claim> UserClaims => _loginResult.User.Claims;

    }
}
