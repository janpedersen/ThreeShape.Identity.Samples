using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OAuthWpfApp.Login
{
    public class SystemWebBrowser : IBrowser
    {

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var result = new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };

            var listener = new HttpListener();
            listener.Prefixes.Add(options.EndUrl);
            listener.Start();

            Process.Start(new ProcessStartInfo(options.StartUrl)
            {
                UseShellExecute = true
            });


            var context = await listener.GetContextAsync();
            if (context != null)
            {
                result = new BrowserResult
                {
                    ResultType = BrowserResultType.Success,
                    Response = context.Request.Url.OriginalString

                };
            }

            return result;
        }

    }
}