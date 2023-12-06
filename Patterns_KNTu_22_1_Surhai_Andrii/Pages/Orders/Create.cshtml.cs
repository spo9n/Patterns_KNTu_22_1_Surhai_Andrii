using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IObserver _observer;
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


        public CreateModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._orderDAO = _daoFactory.CreateOrderDAO();
            this._orderDetailDAO = _daoFactory.CreateOrderDetailDAO();
            this._orderStatusDAO = _daoFactory.CreateOrderStatusDAO();
            this._instrumentDAO = _daoFactory.CreateInstrumentDAO();
            this._userDAO = _daoFactory.CreateUserDAO();
            this._observer = observer;
            this._orderDAO.AddObserver(_observer);
            this._orderDetailDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Orders = _orderDAO.GetAll();
            OrdersDetails = _orderDetailDAO.GetAll();
            OrdersStatuses = _orderStatusDAO.GetAll();
            Instruments = _instrumentDAO.GetAll();
            Users = _userDAO.GetAll();
        }

        public void OnPostCreate()
        {
            int userId = Convert.ToInt32(Request.Form["user_id"]);
            int statusId = 1;
            string comment = Convert.ToString(Request.Form["comment"]);

            Order = new Order.Builder()
                .WithUserId(userId)
                .WithStatusId(statusId)
                .WithComment(comment)
                .Build();

            int orderId = _orderDAO.Create(Order);

            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("instrumentId_"))
                {
                    int instrumentId = Convert.ToInt32(Request.Form[key]);
                    double price = Convert.ToDouble(Request.Form[$"instrumentPrice_{instrumentId}"]);
                    int quantity = Convert.ToInt32(Request.Form[$"instrumentQuantity_{instrumentId}"]);

                    OrderDetail = new OrderDetail.Builder()
                        .WithOrderId(orderId)
                        .WithInstrumentId(instrumentId)
                        .WithPrice(price)
                        .WithQuantity(quantity)
                        .Build();

                    _orderDetailDAO.Create(OrderDetail);
                }
            }

            OnGet();
        }
    }
}
