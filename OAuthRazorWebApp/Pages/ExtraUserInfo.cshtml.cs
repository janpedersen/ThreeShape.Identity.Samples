using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace OAuthRazorWebApp.Pages
{
    [Authorize]

    public class ExtraUserInfoModel : PageModel
    {
        private readonly ILogger<ExtraUserInfoModel> _logger;

        public ExtraUserInfoModel(ILogger<ExtraUserInfoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
