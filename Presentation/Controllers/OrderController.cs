using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data.Interfaces;
using Presentation.Data.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBikeRepo _bikeRepo;
        private readonly Order _order;

        public OrderController(IBikeRepo bikeRepo, Order order)
        {
            _bikeRepo = bikeRepo;
            _order = order;
        }

        public IActionResult Index()
        {
            var items = _order.GetOrderItems();
            _order.OrderItems = items;

            var vm = new OrderVM
            {
                Order = _order,
                TotalOrder = _order.GetTotalOrder()
            };
            return View(vm);
        }

        public RedirectToActionResult AddToOrder(int bikeID)
        {
            var selectedBike = _bikeRepo.Bikes.FirstOrDefault(p => p.BikeID == bikeID);
            if (selectedBike != null)
            {
                _order.AddToCart(selectedBike, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult DeleteOrder(int bikeID)
        {
            var selectedBike = _bikeRepo.Bikes.FirstOrDefault(p => p.BikeID == bikeID);
            if (selectedBike != null)
            {
                _order.RemoveFromCart(selectedBike);
            }
            return RedirectToAction("Index");

        }


    }
}