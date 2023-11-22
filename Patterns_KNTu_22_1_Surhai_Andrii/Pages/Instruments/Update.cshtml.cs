using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Instruments
{
    public class UpdateModel : PageModel
    {
        private readonly IDAOFactory _daoFactory;
        private readonly IInstrumentDAO _instrumentDAO;
        private readonly ICategoryDAO _categoryDAO;
        private readonly IBrandDAO _brandDAO;
        private readonly ICountryDAO _countryDAO;

        public Instrument Instrument { get; set; }

        public List<Instrument> Instruments { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Country> Countries { get; set; }


        public UpdateModel(IDAOFactory daoFactory)
        {
            this._daoFactory = daoFactory;
            this._instrumentDAO = _daoFactory.CreateInstrumentDAO();
            this._categoryDAO = _daoFactory.CreateCategoryDAO();
            this._brandDAO = _daoFactory.CreateBrandDAO();
            this._countryDAO = _daoFactory.CreateCountryDAO();
        }


        public void OnGet()
        {
            Instruments = _instrumentDAO.GetAll();
            Categories = _categoryDAO.GetAll();
            Brands = _brandDAO.GetAll();
            Countries = _countryDAO.GetAll();
        }


        public void OnPostUpdate()
        {
            Instrument = new Instrument.Builder()
                .WithId(Convert.ToInt32(Request.Form["instrument_id"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .WithCategoryId(Convert.ToInt32(Request.Form["category_id"]))
                .WithBrandId(Convert.ToInt32(Request.Form["brand_id"]))
                .WithCountryId(Convert.ToInt32(Request.Form["country_id"]))
                .WithYear(Convert.ToInt32(Request.Form["year"]))
                .WithPrice(Convert.ToDouble(Request.Form["price"]))
                .WithQuantity(Convert.ToInt32(Request.Form["quantity"]))
                .WithDescription(Convert.ToString(Request.Form["description"]))
                .Build();

            _instrumentDAO.Update(Instrument);

            OnGet();
        }
    }
}
