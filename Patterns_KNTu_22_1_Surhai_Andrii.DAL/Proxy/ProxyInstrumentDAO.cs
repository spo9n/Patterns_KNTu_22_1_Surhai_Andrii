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
            ///
            this._instrumentDAO = new DAOFactory().CreateInstrumentDAO();
            ///

            this._userRole = userRole;
        }

        public void Create(Instrument instrument)
        {
            //if (_userRole == Admin)
            //{
            //    _instrumentDAO.Create(instrument);
            //}
            //else
            //{
            //    exception
            //}
            //throw new NotImplementedException();
        }

        public void Update(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Instrument> GetAll()
        {
            throw new NotImplementedException();
        }

        public Instrument GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Notify(string message)
        {
            throw new NotImplementedException();
        }
    }
}
