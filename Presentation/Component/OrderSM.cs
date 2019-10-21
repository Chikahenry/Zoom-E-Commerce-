using Microsoft.AspNetCore.Mvc;
using Presentation.Data.Models;
using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Component
{
    public class OrderSM : ViewComponent
    {
        private readonly Order _order;

        public OrderSM(Order order)
        {
            _order = order;

        }

        public IViewComponentResult Invoke()
        {
            var items = _order.GetOrderItems();
            _order.OrderItems = items;

            var orderVM = new OrderVM
            {
                Order = _order,
                TotalOrder = _order.GetTotalOrder()
            };
            return View(orderVM);

        }

    }
}
