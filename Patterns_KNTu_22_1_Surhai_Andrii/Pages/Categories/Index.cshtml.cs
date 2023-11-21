using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryDAO _categoryDAO;

        public Category Category { get; set; }

        public List<Category> Categories { get; set; }


        public IndexModel(ICategoryDAO categoryDAO)
        {
            this._categoryDAO = categoryDAO;
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