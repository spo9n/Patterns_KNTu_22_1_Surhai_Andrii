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
            User = new User.Builder()
                .WithId(Convert.ToInt32(Request.Form["user_id"]))
                .WithSurname(Convert.ToString(Request.Form["surname"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .WithPatronymic(Convert.ToString(Request.Form["patronymic"]))
                .WithEmail(Convert.ToString(Request.Form["email"]))
                .WithPhone(Convert.ToString(Request.Form["phone"]))
                .Build();

            _userDAO.Update(User);

            OnGet();
        }
    }
}
