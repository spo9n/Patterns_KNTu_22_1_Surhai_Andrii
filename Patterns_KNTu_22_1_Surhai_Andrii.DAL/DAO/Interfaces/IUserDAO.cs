using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IUserDAO : IDAOObservable
    {
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        User GetById(int id);
        User GetByUsername(string username);
        List<User> GetAll();
    }
}
