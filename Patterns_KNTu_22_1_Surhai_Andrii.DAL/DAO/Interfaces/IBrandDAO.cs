using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IBrandDAO
    {
        void Create(Brand brand);
        void Update(Brand brand);
        void Delete(int id);
        Brand GetById(int id);
        List<Brand> GetAll();
    }
}
