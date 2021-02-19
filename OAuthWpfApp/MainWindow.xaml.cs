using OAuthWpfApp.Login;
using System;
using System.Linq;
using System.Windows;

namespace OAuthWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var loginWithEmbeddedBrowser = WpfRadio.IsChecked ?? false;
            var loginType = loginWithEmbeddedBrowser ? "Embedded Browser" : "System Browser";

            TextLog.Text += $"Authenticated with: {loginType} \n";
            TextLog.Text += "Login process is started..... \n";

            try
            {
                var identityHandler = new IdentityHandler(loginWithEmbeddedBrowser);

                identityHandler.InitLogin();

                await identityHandler.DoLoginAsync();

                if (identityHandler.IsLoginSuccessful)
                {
                    TextLog.Text += "User claims: \n";
                    ShowUserClaims(identityHandler);

                    TextLog.Text += $"RefreshToken: {identityHandler.RefreshToken} \n";
                    TextLog.Text += $"AccessToken Expires At: {identityHandler.AccessTokenExpiresAt} \n";
                    TextLog.Text += $"AccessToken: {identityHandler.AccessToken} \n";
                }
                else
                {
                    TextLog.Text += $"Login Error: {identityHandler.LoginError} \n";
                }
            }
            catch (Exception ex)
            {
                TextLog.Text += $"Unexpected Error: {ex.Message} \n";
            }

            TextLog.Text += "----------------End of login call---------------- \n";
        }

        private void ShowUserClaims(IdentityHandler identityHandler)
        {

            foreach (var userClaim in identityHandler.UserClaims.OrderBy(x => x.Type))
            {
                var claimName = userClaim.Type;
                var claimValue = userClaim.Value;

                if (claimName == "sub")
                {
                    claimValue += "  <---- UserId";
                }

                if (claimName == "selectedCompanyId")
                {
                    continue; // deprecated claim. use CompanyId instead
                }

                TextLog.Text += $"        {claimName} : {claimValue} \n";
            }
        }
    }
}
