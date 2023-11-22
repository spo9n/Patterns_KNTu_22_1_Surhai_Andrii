using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IOrderDetailDAO
    {
        void Create(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(int orderId);
        List<OrderDetail> GetById(int orderId);
        List<OrderDetail> GetAll();
    }
}
