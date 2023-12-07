using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Proxy;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory
{
    public class DAOFactory : IDAOFactory
    {
        public IBrandDAO CreateBrandDAO()
        {
            return new BrandDAO();
        }

        public ICategoryDAO CreateCategoryDAO()
        {
            return new CategoryDAO();
        }

        public ICountryDAO CreateCountryDAO()
        {
            return new CountryDAO();
        }

        public IInstrumentDAO CreateInstrumentDAO()
        {
            return new InstrumentDAO();
        }

        public IInstrumentDAO CreateProxyInstrumentDAO(UserRole userRole)
        {
            return new ProxyInstrumentDAO(userRole);
        }

        public IOrderDAO CreateOrderDAO()
        {
            return new OrderDAO();
        }

        public IOrderDetailDAO CreateOrderDetailDAO()
        {
            return new OrderDetailDAO();
        }

        public IOrderStatusDAO CreateOrderStatusDAO()
        {
            return new OrderStatusDAO();
        }

        public IUserDAO CreateUserDAO()
        {
            return new UserDAO();
        }

        public IUserRoleDAO CreateUserRoleDAO()
        {
            return new UserRoleDAO();
        }
    }
}
