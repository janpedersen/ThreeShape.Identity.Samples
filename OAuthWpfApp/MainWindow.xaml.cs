using OAuthWpfApp.Login;
using System;
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
            var newLine = Environment.NewLine;

            TextLog.Text += "Login started....." + newLine;

            var loginWithEmbeddedBrowser = (WpfRadio.IsChecked ?? false);

            try
            {
                var identityHandler = new IdentityHandler(loginWithEmbeddedBrowser);

                identityHandler.InitLogin();

                await identityHandler.DoLoginAsync();

                if (identityHandler.IsLoginSuccessful)
                {
                    TextLog.Text += $"Hello {identityHandler.LoggedInUser.FullName}" + newLine;

                    TextLog.Text += $"Email: {identityHandler.LoggedInUser.Email}" + newLine;

                    TextLog.Text += $"UserId: {identityHandler.LoggedInUser.Id}" + newLine;

                    TextLog.Text += $"CompanyId: {identityHandler.LoggedInUser.CompanyId}" + newLine;

                    TextLog.Text += $"UserIp: {identityHandler.LoggedInUser.Ip}" + newLine;

                    TextLog.Text += $"Roles: {identityHandler.LoggedInUser.Roles}" + newLine;

                    TextLog.Text += $"TokenExpiresAt: {identityHandler.LoggedInUser.TokenExpiresAt}" + newLine;

                    TextLog.Text += $"Token: {identityHandler.LoggedInUser.Token}" + newLine;
                }
                else
                {
                    TextLog.Text += $"Login Error: {identityHandler.LoginError}";
                }
            }
            catch (Exception ex)
            {
                TextLog.Text += $"Unexpected Error: {ex.Message}" + newLine;
            }

            TextLog.Text += "----------------End of login call----------------" + newLine;
        }


    }
}
