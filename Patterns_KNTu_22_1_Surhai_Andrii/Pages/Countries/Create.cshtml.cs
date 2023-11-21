using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Countries
{
    public class CreateModel : PageModel
    {
        private readonly ICountryDAO _countryDAO;

        public Country Country { get; set; }

        public List<Country> Countries { get; set; }


        public CreateModel(ICountryDAO countryDAO)
        {
            this._countryDAO = countryDAO;
        }


        public void OnGet()
        {
            Countries = _countryDAO.GetAll();
        }


        public void OnPostCreate()
        {
            Country = new Country.Builder()
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _countryDAO.Create(Country);

            OnGet();
        }
    }
}
