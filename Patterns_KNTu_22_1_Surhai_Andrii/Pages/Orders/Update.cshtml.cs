using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Orders
{
    public class UpdateModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IOrderDAO _orderDAO;
        private readonly IOrderStatusDAO _orderStatusDAO;
        private readonly IUserDAO _userDAO;

        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderStatus> OrdersStatuses { get; set; }
        public List<User> Users { get; set; }


        public UpdateModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._orderDAO = _daoFactory.CreateOrderDAO();
            this._orderStatusDAO = _daoFactory.CreateOrderStatusDAO();
            this._userDAO = _daoFactory.CreateUserDAO();
            this._observer = observer;
            this._orderDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            Orders = _orderDAO.GetAll();
            OrdersStatuses = _orderStatusDAO.GetAll();
            Users = _userDAO.GetAll();
        }

        public void OnPostUpdate()
        {
            int id = Convert.ToInt32(Request.Form["order_id"]);
            int userId = Convert.ToInt32(Request.Form["user_id"]);
            int statusId = Convert.ToInt32(Request.Form["status_id"]);
            string comment = Convert.ToString(Request.Form["comment"]);

            Order = new Order.Builder()
                .WithId(id)
                .WithUserId(userId)
                .WithStatusId(statusId)
                .WithComment(comment)
                .Build();

            _orderDAO.Update(Order);

            OnGet();
        }
    }
}
