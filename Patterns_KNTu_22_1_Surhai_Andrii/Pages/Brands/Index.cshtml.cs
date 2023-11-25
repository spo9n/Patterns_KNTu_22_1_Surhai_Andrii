using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IBrandDAO _brandDAO;

        public Brand Brand { get; set; }
        public List<Brand> Brands { get; set; }


        public IndexModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._brandDAO = _daoFactory.CreateBrandDAO();
            this._observer = observer;

            this._brandDAO.AddObserver(_observer);
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
