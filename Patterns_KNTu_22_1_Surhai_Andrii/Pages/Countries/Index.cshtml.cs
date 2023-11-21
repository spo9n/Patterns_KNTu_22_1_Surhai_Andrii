using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Countries
{
    public class IndexModel : PageModel
    {
        private readonly ICountryDAO _countryDAO;

        public Country Country { get; set; }

        public List<Country> Countries { get; set; }


        public IndexModel(ICountryDAO countryDAO)
        {
            this._countryDAO = countryDAO;
        }


        public void OnGet()
        {
            Countries = _countryDAO.GetAll();
        }


        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["country_id_delete"]);
            _countryDAO.Delete(id);

            OnGet();
        }


        public JsonResult OnGetGetCountryById(int id)
        {
            Country = _countryDAO.GetById(id);
            return new JsonResult(Country);
        }
    }
}
