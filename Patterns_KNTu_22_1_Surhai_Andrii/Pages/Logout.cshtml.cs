using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.SetString("IsAuthorized", "No");
            HttpContext.Session.SetString("UserRole", "{\"Id\":1,\"Name\":\"User\"}");

            return RedirectToPage("/Index");
        }
    }
}
