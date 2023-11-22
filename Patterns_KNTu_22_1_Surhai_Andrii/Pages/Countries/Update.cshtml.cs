using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Countries
{
    public class UpdateModel : PageModel
    {
        private readonly IDAOFactory _daoFactory;
        private readonly ICountryDAO _countryDAO;

        public Country Country { get; set; }

        public List<Country> Countries { get; set; }


        public UpdateModel(IDAOFactory daoFactory)
        {
            this._daoFactory = daoFactory;
            this._countryDAO = _daoFactory.CreateCountryDAO();
        }


        public void OnGet()
        {
            Countries = _countryDAO.GetAll();
        }


        public void OnPostUpdate()
        {
            Country = new Country.Builder()
                .WithId(Convert.ToInt32(Request.Form["country_id"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _countryDAO.Update(Country);

            OnGet();
        }
    }
}
