using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces
{
    public interface IOrderDetailDAO : IDAOObservable
    {
        void Create(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(int orderId);
        List<OrderDetail> GetById(int orderId);
        List<OrderDetail> GetAll();
    }
}
