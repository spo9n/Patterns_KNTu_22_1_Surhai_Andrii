using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory
{
    public interface IDAOFactory
    {
        IBrandDAO CreateBrandDAO();
        ICategoryDAO CreateCategoryDAO();
        ICountryDAO CreateCountryDAO();
        IInstrumentDAO CreateInstrumentDAO();
        IInstrumentDAO CreateProxyInstrumentDAO(UserRole userRole);
        IOrderDAO CreateOrderDAO();
        IOrderDetailDAO CreateOrderDetailDAO();
        IOrderStatusDAO CreateOrderStatusDAO();
        IUserDAO CreateUserDAO();
        IUserRoleDAO CreateUserRoleDAO();
    }
}
