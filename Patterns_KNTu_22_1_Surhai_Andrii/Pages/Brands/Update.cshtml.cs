using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Brands
{
    public class UpdateModel : PageModel
    {
        private readonly IDAOFactory _daoFactory;
        private readonly IBrandDAO _brandDAO;

        public Brand Brand { get; set; }

        public List<Brand> Brands { get; set; }


        public UpdateModel(IDAOFactory daoFactory)
        {
            this._daoFactory = daoFactory;
            this._brandDAO = _daoFactory.CreateBrandDAO();
        }


        public void OnGet()
        {
            Brands = _brandDAO.GetAll();
        }


        public void OnPostUpdate()
        {
            Brand = new Brand.Builder()
                .WithId(Convert.ToInt32(Request.Form["brand_id"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _brandDAO.Update(Brand);

            OnGet();
        }
    }
}
