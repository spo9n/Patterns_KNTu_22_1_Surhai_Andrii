using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.OrdersStatuses
{
    public class UpdateModel : PageModel
    {
        private readonly IOrderStatusDAO _orderStatusDAO;

        public OrderStatus OrderStatus { get; set; }

        public List<OrderStatus> OrdersStatuses { get; set; }


        public UpdateModel(IOrderStatusDAO orderStatusDAO)
        {
            this._orderStatusDAO = orderStatusDAO;
        }


        public void OnGet()
        {
            OrdersStatuses = _orderStatusDAO.GetAll();
        }


        public void OnPostUpdate()
        {
            OrderStatus = new OrderStatus.Builder()
                .WithId(Convert.ToInt32(Request.Form["status_id"]))
                .WithName(Convert.ToString(Request.Form["name"]))
                .Build();

            _orderStatusDAO.Update(OrderStatus);

            OnGet();
        }
    }
}
