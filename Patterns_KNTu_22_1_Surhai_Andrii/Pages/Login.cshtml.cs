using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IDAOFactory _daoFactory;
        private readonly IUserDAO _userDAO;
        private readonly IUserRoleDAO _userRoleDAO;

        public User User { get; set; }
        public UserRole UserRole { get; set; }


        public LoginModel(IDAOFactory daoFactory)
        {
            _userDAO = daoFactory.CreateUserDAO();
            _userRoleDAO = daoFactory.CreateUserRoleDAO();
        }

        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            User = _userDAO.GetByUsername(username);

            if (User != null && User.Password == password)
            {
                UserRole = _userRoleDAO.GetById(User.UserRoleId);

                if (UserRole != null)
                {
                    string serializedUserRole = JsonConvert.SerializeObject(UserRole);
                    HttpContext.Session.SetString("UserRole", serializedUserRole);
                    HttpContext.Session.SetString("IsAuthorized", "Yes");
                }

                return RedirectToPage("/Instruments/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect Login or Password!");
                return Page();
            }
        }
    }
}
