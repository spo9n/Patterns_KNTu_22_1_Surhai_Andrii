using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Brands
{
    public class CreateModel : PageModel
    {
        private readonly IBrandDAO _brandDAO;

        public Brand Brand { get; set; }

        public List<Brand> Brands { get; set; }


        public CreateModel(IBrandDAO brandDAO)
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

            OnGet();
        }
    }
}
