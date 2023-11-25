using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly ICategoryDAO _categoryDAO;

        public Category Category { get; set; }
        public List<Category> Categories { get; set; }


        public IndexModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._categoryDAO = _daoFactory.CreateCategoryDAO();
            this._observer = observer;

            this._categoryDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Categories = _categoryDAO.GetAll();
        }

        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["category_id_delete"]);
            _categoryDAO.Delete(id);

            OnGet();
        }

        public JsonResult OnGetGetCategoryById(int id)
        {
            Category = _categoryDAO.GetById(id);
            return new JsonResult(Category);
        }
    }
}
