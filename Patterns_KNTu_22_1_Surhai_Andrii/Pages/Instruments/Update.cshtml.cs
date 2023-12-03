using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public Instrument Instrument { get; set; }
        public List<Instrument> Instruments { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Country> Countries { get; set; }
        public InstrumentCaretaker InstrumentCaretaker { get; private set; }



        public UpdateModel(IDAOFactory daoFactory, IObserver observer, InstrumentCaretaker instrumentCaretaker)
        {
            this._daoFactory = daoFactory;
            this._instrumentDAO = _daoFactory.CreateInstrumentDAO();
            this._categoryDAO = _daoFactory.CreateCategoryDAO();
            this._brandDAO = _daoFactory.CreateBrandDAO();
            this._countryDAO = _daoFactory.CreateCountryDAO();

            this._observer = observer;
            this._instrumentDAO.AddObserver(_observer);

            this.InstrumentCaretaker = instrumentCaretaker;
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
            int instrumentId = Convert.ToInt32(Request.Form["instrument_id"]);
            Instrument instrumentBeforeUpdate = _instrumentDAO.GetById(instrumentId);

            InstrumentMemento instrumentMemento = instrumentBeforeUpdate.CreateMemento();
            InstrumentCaretaker.AddMemento(instrumentMemento);

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

        public void OnPostUndo()
        {
            if (InstrumentCaretaker.Mementos.Count > 0)
            {
                InstrumentMemento instrumentMemento = InstrumentCaretaker.GetMemento();

                if (_instrumentDAO.GetById(instrumentMemento.Id) != null)
                {
                    Instrument = new Instrument.Builder().BuildEmpty();
                    Instrument.RestoreMemento(instrumentMemento);

                    _instrumentDAO.Update(Instrument);
                }
            }

            OnGet();
        }
    }
}
