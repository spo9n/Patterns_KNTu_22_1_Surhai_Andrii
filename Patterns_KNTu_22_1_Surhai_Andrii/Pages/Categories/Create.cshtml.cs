using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryDAO _categoryDAO;

        public Category Category { get; set; }

        public List<Category> Categories { get; set; }


        public CreateModel(ICategoryDAO categoryDAO)
        {
            this._categoryDAO = categoryDAO;
        }


        public void OnGet()
        {
            Categories = _categoryDAO.GetAll();
        }


        public void OnPostCreate()
        {
            Category = new Category.Builder()
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _categoryDAO.Create(Category);

            OnGet();
        }
    }
}
