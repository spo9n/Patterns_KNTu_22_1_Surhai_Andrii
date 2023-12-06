using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IUserDAO _userDAO;

        public User User { get; set; }
        public List<User> Users { get; set; }


        public UpdateModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._userDAO = _daoFactory.CreateUserDAO();
            this._observer = observer;
            this._userDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Users = _userDAO.GetAll();
        }

        public void OnPostUpdate()
        {
            int id = Convert.ToInt32(Request.Form["user_id"]);
            int userRoleId = Convert.ToInt32(Request.Form["user_role_id"]);
            string surname = Convert.ToString(Request.Form["surname"]);
            string name = Convert.ToString(Request.Form["name"]);
            string patronymic = Convert.ToString(Request.Form["patronymic"]);
            string email = Convert.ToString(Request.Form["email"]);
            string phone = Convert.ToString(Request.Form["phone"]);

            User = new User.Builder()
                .WithId(id)
                .WithUserRoleId(userRoleId)
                .WithSurname(surname)
                .WithName(name)
                .WithPatronymic(patronymic)
                .WithEmail(email)
                .WithPhone(phone)
                .Build();

            _userDAO.Update(User);

            OnGet();
        }
    }
}
