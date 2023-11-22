using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;

namespace Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory
{
    public interface IDAOFactory
    {
        IBrandDAO CreateBrandDAO();
        ICategoryDAO CreateCategoryDAO();
        ICountryDAO CreateCountryDAO();
        IInstrumentDAO CreateInstrumentDAO();
        IOrderDAO CreateOrderDAO();
        IOrderDetailDAO CreateOrderDetailDAO();
        IOrderStatusDAO CreateOrderStatusDAO();
        IUserDAO CreateUserDAO();
    }
}
