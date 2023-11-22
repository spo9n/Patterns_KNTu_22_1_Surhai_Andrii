using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IDAOFactory _daoFactory;
        private readonly IOrderDAO _orderDAO;
        private readonly IOrderDetailDAO _orderDetailDAO;
        private readonly IOrderStatusDAO _orderStatusDAO;
        private readonly IInstrumentDAO _instrumentDAO;
        private readonly IUserDAO _userDAO;


        public Order Order { get; set; }
        public OrderDetail OrderDetail { get; set; }

        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrdersDetails { get; set; }
        public List<OrderStatus> OrdersStatuses { get; set; }
        public List<Instrument> Instruments { get; set; }
        public List<User> Users { get; set; }


        public IndexModel(IDAOFactory daoFactory)
        {
            this._daoFactory = daoFactory;
            this._orderDAO = _daoFactory.CreateOrderDAO();
            this._orderDetailDAO = _daoFactory.CreateOrderDetailDAO();
            this._orderStatusDAO = _daoFactory.CreateOrderStatusDAO();
            this._instrumentDAO = _daoFactory.CreateInstrumentDAO();
            this._userDAO = _daoFactory.CreateUserDAO();
        }


        public void OnGet()
        {
            Orders = _orderDAO.GetAll();
            OrdersDetails = _orderDetailDAO.GetAll();
            OrdersStatuses = _orderStatusDAO.GetAll();
            Instruments = _instrumentDAO.GetAll();
            Users = _userDAO.GetAll();
        }


        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["order_id_delete"]);
            _orderDAO.Delete(id);

            OnGet();
        }


        public JsonResult OnGetGetOrderDetails(int id)
        {
            List<OrderDetail> orderDetails = _orderDetailDAO.GetById(id);
            return new JsonResult(orderDetails);
        }


        public JsonResult OnGetGetOrderById(int id)
        {
            Order = _orderDAO.GetById(id);
            return new JsonResult(Order);
        }
    }
}
