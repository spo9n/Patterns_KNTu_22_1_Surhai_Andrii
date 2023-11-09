using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IUserDAO
    {
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        User GetById(int id);
        List<User> GetAll();
    }
}
