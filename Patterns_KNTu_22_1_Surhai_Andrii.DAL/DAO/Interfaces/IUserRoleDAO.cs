using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IUserRoleDAO : IDAOObservable
    {
        void Create(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(int id);
        UserRole GetById(int id);
        List<UserRole> GetAll();
    }
}
