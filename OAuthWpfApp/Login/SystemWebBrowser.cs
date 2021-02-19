using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using System.Net;
using System.Text;
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
                ResultType = BrowserResultType.UnknownError
            };

            Process.Start(new ProcessStartInfo(options.StartUrl)
            {
                UseShellExecute = true
            });

            //start a local server which listen to http://localhost:port
            var listener = new HttpListener();
            listener.Prefixes.Add(options.EndUrl);
            listener.Start();

            //waiting for a call back from Identity
            var context = await listener.GetContextAsync();
            if (context != null)
            {
                result = new BrowserResult
                {
                    ResultType = BrowserResultType.Success,
                    //return the call back url which contain the token
                    Response = context.Request.Url.OriginalString

                };

                ShowTheSuccessfulMessageInBrowser(context.Response);
            }

            listener.Stop();
            return result;
        }
        private void ShowTheSuccessfulMessageInBrowser(HttpListenerResponse response)
        {
            var responseMessage = Encoding.UTF8.GetBytes(
                "<h1>Login Completed</1><h2>You can now close this page and return back to the application.</h2>");

            response.ContentType = "text/html";
            response.ContentLength64 = responseMessage.Length;
            response.StatusCode = 200;
            response.OutputStream.Write(responseMessage, 0, responseMessage.Length);
            response.OutputStream.Close();
        }
    }
}