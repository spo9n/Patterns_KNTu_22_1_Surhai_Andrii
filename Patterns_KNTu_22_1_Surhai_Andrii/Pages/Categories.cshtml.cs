using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly IBrandDAO _brandDAO;

        public Brand Brand { get; set; }

        public List<Brand> Brands { get; set; }


        public CategoryModel(IBrandDAO brandDAO)
        {
            this._brandDAO = brandDAO;
        }


        public void OnGet()
        {
            Brands = _brandDAO.GetAll();
        }


        public void OnPostCreate()
        {
            Brand = new Brand.Builder()
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _brandDAO.Create(Brand);
        }


        public void OnPostDelete()
        {
            _brandDAO.Delete(Convert.ToInt32(Request.Form["category_id_delete"]));
        }


        public void OnPostSelectById()
        {
            Brand = _brandDAO.GetById(Convert.ToInt32(Request.Form["brand_id_select"]));
        }


        public void OnPostUpdate()
        {
            Brand = new Brand.Builder()
                .WithId(Convert.ToInt32(Request.Form["brand_id_update"]))
                .WithName(Convert.ToString(Request.Form["name_update"]))
                .Build();

            _brandDAO.Update(Brand);
        }

    }
}
