using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IOrderDAO
    {
        void Create(Order Order);
        void Update(Order Order);
        void Delete(int id);
        Order GetById(int id);
        List<Order> GetAll();
    }
}
