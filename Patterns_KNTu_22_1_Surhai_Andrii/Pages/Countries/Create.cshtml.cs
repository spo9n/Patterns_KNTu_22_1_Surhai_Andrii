using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Countries
{
    public class CreateModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly ICountryDAO _countryDAO;

        public Country Country { get; set; }
        public List<Country> Countries { get; set; }


        public CreateModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._countryDAO = _daoFactory.CreateCountryDAO();
            this._observer = observer;
            this._countryDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Countries = _countryDAO.GetAll();
        }

        public void OnPostCreate()
        {
            string name = Convert.ToString(Request.Form["name"]);

            Country = new Country.Builder()
                .WithName(name)
                .Build();

            _countryDAO.Create(Country);

            OnGet();
        }
    }
}
