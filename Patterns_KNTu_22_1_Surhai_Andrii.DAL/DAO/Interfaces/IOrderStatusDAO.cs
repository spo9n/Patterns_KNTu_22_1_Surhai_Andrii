using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IOrderStatusDAO
    {
        void Create(OrderStatus orderStatus);
        void Update(OrderStatus orderStatus);
        void Delete(int id);
        OrderStatus GetById(int id);
        List<OrderStatus> GetAll();
    }
}
