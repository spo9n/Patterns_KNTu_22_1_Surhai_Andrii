using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.Proxy
{
    public class ProxyInstrumentDAO : IInstrumentDAO
    {
        private IInstrumentDAO _instrumentDAO;
        private UserRole _userRole;


        public ProxyInstrumentDAO(UserRole userRole)
        {
            this._instrumentDAO = new DAOFactory().CreateInstrumentDAO();
            this._userRole = userRole;
        }

        public void Create(Instrument instrument)
        {
            if (_userRole.Name == "Admin")
            {
                _instrumentDAO.Create(instrument);
            }
            else
            {
                string errorMessage = "Access denied! Only Admin can create Instruments.";
                Console.WriteLine(errorMessage);
                throw new AccessViolationException(errorMessage);
            }
        }

        public void Update(Instrument instrument)
        {
            if (_userRole.Name == "Admin")
            {
                _instrumentDAO.Update(instrument);
            }
            else
            {
                string errorMessage = "Access denied! Only Admin can update Instruments.";
                Console.WriteLine(errorMessage);
                throw new AccessViolationException(errorMessage);
            }

        }

        public void Delete(int id)
        {
            if (_userRole.Name == "Admin")
            {
                _instrumentDAO.Delete(id);
            }
            else
            {
                string errorMessage = "Access denied! Only Admin can delete Instruments.";
                Console.WriteLine(errorMessage);
                throw new AccessViolationException(errorMessage);
            }
        }

        public List<Instrument> GetAll()
        {
            List<Instrument> instruments = _instrumentDAO.GetAll();

            return instruments;
        }

        public Instrument GetById(int id)
        {
            Instrument instrument = _instrumentDAO.GetById(id);

            return instrument;
        }

        public void AddObserver(IObserver observer)
        {
            _instrumentDAO.AddObserver(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _instrumentDAO.RemoveObserver(observer);
        }

        public void Notify(string message)
        {
            _instrumentDAO.Notify(message);
        }
    }
}
