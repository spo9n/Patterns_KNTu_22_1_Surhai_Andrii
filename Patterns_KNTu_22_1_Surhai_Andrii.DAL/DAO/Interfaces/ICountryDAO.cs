using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface ICountryDAO : IDAOObservable
    {
        void Create(Country country);
        void Update(Country country);
        void Delete(int id);
        Country GetById(int id);
        List<Country> GetAll();
    }
}
