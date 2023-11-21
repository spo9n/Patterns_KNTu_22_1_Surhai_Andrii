using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Orders
{
    public class IndexModel : PageModel
    {
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


        public IndexModel(IOrderDAO orderDAO, IOrderDetailDAO orderDetailDAO, IOrderStatusDAO orderStatusDAO, IInstrumentDAO instrumentDAO, IUserDAO userDAO)
        {
            this._orderDAO = orderDAO;
            this._orderDetailDAO = orderDetailDAO;
            this._orderStatusDAO = orderStatusDAO;
            this._instrumentDAO = instrumentDAO;
            this._userDAO = userDAO;
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


        public JsonResult OnGetGetOrderById(int id)
        {
            Order = _orderDAO.GetById(id);
            return new JsonResult(Order);
        }
    }
}
