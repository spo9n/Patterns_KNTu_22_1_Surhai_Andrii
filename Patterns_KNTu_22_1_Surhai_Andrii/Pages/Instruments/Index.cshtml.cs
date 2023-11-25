using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Instruments
{
    public class IndexModel : PageModel
    {
        private readonly IObserver _observer;
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


        public IndexModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._instrumentDAO = _daoFactory.CreateInstrumentDAO();
            this._categoryDAO = _daoFactory.CreateCategoryDAO();
            this._brandDAO = _daoFactory.CreateBrandDAO();
            this._countryDAO = _daoFactory.CreateCountryDAO();
            this._observer = observer;

            this._instrumentDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Instruments = _instrumentDAO.GetAll();
            Categories = _categoryDAO.GetAll();
            Brands = _brandDAO.GetAll();
            Countries = _countryDAO.GetAll();
        }

        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["instrument_id_delete"]);
            _instrumentDAO.Delete(id);

            OnGet();
        }

        public JsonResult OnGetGetInstrumentById(int id)
        {
            Instrument = _instrumentDAO.GetById(id);
            return new JsonResult(Instrument);
        }
    }
}
