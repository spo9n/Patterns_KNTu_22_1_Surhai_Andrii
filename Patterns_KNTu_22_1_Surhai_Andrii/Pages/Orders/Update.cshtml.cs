using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.Orders
{
    public class UpdateModel : PageModel
    {
        private readonly IOrderDAO _orderDAO;
        private readonly IOrderStatusDAO _orderStatusDAO;
        private readonly IUserDAO _userDAO;

        public Order Order { get; set; }

        public List<Order> Orders { get; set; }
        public List<OrderStatus> OrdersStatuses { get; set; }
        public List<User> Users { get; set; }


        public UpdateModel(IOrderDAO orderDAO, IOrderStatusDAO orderStatusDAO, IUserDAO userDAO)
        {
            this._orderDAO = orderDAO;
            this._orderStatusDAO = orderStatusDAO;
            this._userDAO = userDAO;
        }


        public void OnGet()
        {
            Orders = _orderDAO.GetAll();
            OrdersStatuses = _orderStatusDAO.GetAll();
            Users = _userDAO.GetAll();
        }


        public void OnPostUpdate()
        {
            Order = new Order.Builder()
                .WithId(Convert.ToInt32(Request.Form["order_id"]))
                .WithUserId(Convert.ToInt32(Request.Form["user_id"]))
                .WithStatusId(Convert.ToInt32(Request.Form["status_id"]))
                .WithComment(Convert.ToString(Request.Form["comment"]))
                .Build();

            _orderDAO.Update(Order);

            OnGet();
        }
    }
}
