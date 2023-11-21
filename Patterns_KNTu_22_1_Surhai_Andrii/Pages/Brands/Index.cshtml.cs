using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly IBrandDAO _brandDAO;

        public Brand Brand { get; set; }

        public List<Brand> Brands { get; set; }


        public IndexModel(IBrandDAO brandDAO)
        {
            this._brandDAO = brandDAO;
        }


        public void OnGet()
        {
            Brands = _brandDAO.GetAll();
        }


        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["brand_id_delete"]);
            _brandDAO.Delete(id);

            OnGet();
        }


        public JsonResult OnGetGetBrandById(int id)
        {
            Brand = _brandDAO.GetById(id);
            return new JsonResult(Brand);
        }
    }
}
