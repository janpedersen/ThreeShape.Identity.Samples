using IdentityModel.OidcClient.Browser;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OAuthWpfApp.Login
{
    public class WpfEmbeddedBrowser : IBrowser
    {

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var window = new Window
            {
                Width = 900,
                Height = 625,
                Title = "Identity Login"
            };

            var webBrowser = new WebBrowser();

            var signal = new SemaphoreSlim(0, 1);

            var result = new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };

            webBrowser.Navigating += (s, e) =>
            {
                if (e.Uri.AbsoluteUri.StartsWith(options.EndUrl))
                {
                    e.Cancel = true;

                    result = new BrowserResult
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Uri.AbsoluteUri
                    };

                    signal.Release();

                    window.Close();
                }
            };

            window.Closing += (s, e) =>
            {
                signal.Release();
            };

            window.Content = webBrowser;
            window.Show();
            webBrowser.Source = new Uri(options.StartUrl);

            await signal.WaitAsync(cancellationToken);

            return result;
        }

    }
}