using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace OauthRazorWebApp2.Pages.Account
{
    public class ManualLoginModel : PageModel
    {
        public IActionResult OnPostAsync()
        {
            // Note: this is hacking way of doing authentication and just here for especial cases
            return Challenge("3ShapeId");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {

            await HttpContext.SignOutAsync("Cookies");
            return RedirectToPage();
        }
    }
}
