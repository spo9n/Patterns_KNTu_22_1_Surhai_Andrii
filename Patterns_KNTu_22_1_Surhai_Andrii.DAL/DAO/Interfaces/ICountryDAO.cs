using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface ICountryDAO
    {
        void Create(Country country);
        void Update(Country country);
        void Delete(int id);
        Country GetById(int id);
        List<Country> GetAll();
    }
}
