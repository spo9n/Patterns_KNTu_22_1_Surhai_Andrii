using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Categories
{
    public class UpdateModel : PageModel
    {
        private readonly ICategoryDAO _categoryDAO;

        public Category Category { get; set; }

        public List<Category> Categories { get; set; }


        public UpdateModel(ICategoryDAO categoryDAO)
        {
            this._categoryDAO = categoryDAO;
        }


        public void OnGet()
        {
            Categories = _categoryDAO.GetAll();
        }


        public void OnPostUpdate()
        {
            Category = new Category.Builder()
                .WithId(Convert.ToInt32(Request.Form["category_id"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _categoryDAO.Update(Category);

            OnGet();
        }
    }
}
