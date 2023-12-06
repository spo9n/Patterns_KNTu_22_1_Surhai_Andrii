using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Brands
{
    public class CreateModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IBrandDAO _brandDAO;

        public Brand Brand { get; set; }
        public List<Brand> Brands { get; set; }


        public CreateModel(IDAOFactory daoFactory, IObserver observer)
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

        public void OnPostCreate()
        {
            string name = Convert.ToString(Request.Form["name"]);

            Brand = new Brand.Builder()
                .WithName(name)
                .Build();

            _brandDAO.Create(Brand);

            OnGet();
        }
    }
}
