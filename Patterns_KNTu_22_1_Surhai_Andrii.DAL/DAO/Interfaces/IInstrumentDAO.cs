using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IInstrumentDAO : IDAOObservable
    {
        void Create(Instrument instrument);
        void Update(Instrument instrument);
        void Delete(int id);
        Instrument GetById(int id);
        List<Instrument> GetAll();
    }
}
