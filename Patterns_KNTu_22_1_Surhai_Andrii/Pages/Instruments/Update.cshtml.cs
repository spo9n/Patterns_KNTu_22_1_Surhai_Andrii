using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Memento;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Instruments
{
    public class UpdateModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IInstrumentDAO _instrumentDAO;
        private readonly ICategoryDAO _categoryDAO;
        private readonly IBrandDAO _brandDAO;
        private readonly ICountryDAO _countryDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Instrument Instrument { get; set; }
        public List<Instrument> Instruments { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Country> Countries { get; set; }
        public InstrumentCaretaker InstrumentCaretaker { get; private set; }
        public UserRole UserRole { get; set; }


        public UpdateModel(IDAOFactory daoFactory, IObserver observer, InstrumentCaretaker instrumentCaretaker, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            string serializedUserRole = _httpContextAccessor.HttpContext.Session.GetString("UserRole");
            UserRole = JsonConvert.DeserializeObject<UserRole>(serializedUserRole);

            _daoFactory = daoFactory;
            //this._instrumentDAO = _daoFactory.CreateInstrumentDAO();
            _instrumentDAO = _daoFactory.CreateProxyInstrumentDAO(UserRole);
            _categoryDAO = _daoFactory.CreateCategoryDAO();
            _brandDAO = _daoFactory.CreateBrandDAO();
            _countryDAO = _daoFactory.CreateCountryDAO();
            _observer = observer;
            _instrumentDAO.AddObserver(_observer);
            InstrumentCaretaker = instrumentCaretaker;
            _httpContextAccessor = httpContextAccessor;
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
            int id = Convert.ToInt32(Request.Form["instrument_id"]);
            string name = Convert.ToString(Request.Form["name"]);
            int categoryId = Convert.ToInt32(Request.Form["category_id"]);
            int brandId = Convert.ToInt32(Request.Form["brand_id"]);
            int countryId = Convert.ToInt32(Request.Form["country_id"]);
            int year = Convert.ToInt32(Request.Form["year"]);
            double price = Convert.ToDouble(Request.Form["price"]);
            int quantity = Convert.ToInt32(Request.Form["quantity"]);
            string description = Convert.ToString(Request.Form["description"]);

            Instrument instrumentBeforeUpdate = _instrumentDAO.GetById(id);
            InstrumentMemento instrumentMemento = instrumentBeforeUpdate.CreateMemento();
            

            Instrument = new Instrument.Builder()
                .WithId(id)
                .WithName(name)
                .WithCategoryId(categoryId)
                .WithBrandId(brandId)
                .WithCountryId(countryId)
                .WithYear(year)
                .WithPrice(price)
                .WithQuantity(quantity)
                .WithDescription(description)
                .Build();

            try
            {
                _instrumentDAO.Update(Instrument);
                InstrumentCaretaker.AddMemento(instrumentMemento);
            }
            catch (AccessViolationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            OnGet();
        }

        public void OnPostUndo()
        {
            if (InstrumentCaretaker.Mementos.Count > 0)
            {
                InstrumentMemento instrumentMemento = InstrumentCaretaker.GetMemento();

                if (_instrumentDAO.GetById(instrumentMemento.Id) != null)
                {
                    Instrument restoredInstrument = new Instrument.Builder().BuildEmpty();
                    restoredInstrument.RestoreMemento(instrumentMemento);
                    _instrumentDAO.Update(restoredInstrument);
                }
            }

            OnGet();
        }
    }
}
