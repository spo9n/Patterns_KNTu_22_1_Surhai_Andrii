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
        private readonly IUserRoleDAO _userRoleDAO;

        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<UserRole> UsersRoles { get; set; }


        public UpdateModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._userDAO = _daoFactory.CreateUserDAO();
            this._userRoleDAO = _daoFactory.CreateUserRoleDAO();
            this._observer = observer;
            this._userDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Users = _userDAO.GetAll();
            UsersRoles = _userRoleDAO.GetAll();
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
            string username = Convert.ToString(Request.Form["username"]);
            string password = Convert.ToString(Request.Form["password"]);

            User = new User.Builder()
                .WithId(id)
                .WithUserRoleId(userRoleId)
                .WithSurname(surname)
                .WithName(name)
                .WithPatronymic(patronymic)
                .WithEmail(email)
                .WithPhone(phone)
                .WithUsername(username)
                .WithPassword(password)
                .Build();

            _userDAO.Update(User);

            OnGet();
        }
    }
}
