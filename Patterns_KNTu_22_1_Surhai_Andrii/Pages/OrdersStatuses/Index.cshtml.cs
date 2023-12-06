using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Factory;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Entities;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.Observer;

namespace Patterns_KNTu_22_1_Surhai_Andrii.Pages.OrdersStatuses
{
    public class IndexModel : PageModel
    {
        private readonly IObserver _observer;
        private readonly IDAOFactory _daoFactory;
        private readonly IOrderStatusDAO _orderStatusDAO;

        public OrderStatus OrderStatus { get; set; }
        public List<OrderStatus> OrdersStatuses { get; set; }


        public IndexModel(IDAOFactory daoFactory, IObserver observer)
        {
            this._daoFactory = daoFactory;
            this._orderStatusDAO = _daoFactory.CreateOrderStatusDAO();
            this._observer = observer;
            this._orderStatusDAO.AddObserver(_observer);
        }

        public void OnGet()
        {
            OrdersStatuses = _orderStatusDAO.GetAll();
        }

        public void OnPostDelete()
        {
            int id = Convert.ToInt32(Request.Form["status_id_delete"]);

            _orderStatusDAO.Delete(id);

            OnGet();
        }

        public JsonResult OnGetGetOrderStatusById(int id)
        {
            OrderStatus = _orderStatusDAO.GetById(id);
            return new JsonResult(OrderStatus);
        }
    }
}
