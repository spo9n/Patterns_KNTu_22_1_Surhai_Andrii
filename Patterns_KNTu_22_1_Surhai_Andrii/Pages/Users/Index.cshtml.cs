using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IUserDAO _userDAO;

        public User User { get; set; }
        public List<User> Users { get; set; }


        public IndexModel(IDAOFactory daoFactory, IObserver observer)
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

        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["user_id_delete"]);
            _userDAO.Delete(id);

            OnGet();
        }

        public JsonResult OnGetGetUserById(int id)
        {
            var user = _userDAO.GetById(id);
            return new JsonResult(user);
        }
    }
}
