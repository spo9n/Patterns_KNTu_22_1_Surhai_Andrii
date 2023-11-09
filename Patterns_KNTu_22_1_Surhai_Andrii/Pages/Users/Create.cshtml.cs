using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserDAO _userDAO;

        public User User { get; set; }

        public List<User> Users { get; set; }


        public CreateModel(IUserDAO userDao)
        {
            this._userDAO = userDao;
        }


        public void OnGet()
        {
            Users = _userDAO.GetAll();
        }


        public void OnPostCreate()
        {
            User = new User.Builder()
                .WithSurname(Convert.ToString(Request.Form["surname"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .WithPatronymic(Convert.ToString(Request.Form["patronymic"]))
                .WithEmail(Convert.ToString(Request.Form["email"]))
                .WithPhone(Convert.ToString(Request.Form["phone"]))
                .Build();

            _userDAO.Create(User);

            OnGet();
        }
    }
}
