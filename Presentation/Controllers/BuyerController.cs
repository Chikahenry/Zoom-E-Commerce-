using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Data.Interfaces;
using Presentation.Data.Models;

namespace Presentation.Controllers
{
    [Authorize]
    public class BuyerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBuyerRepo _buyerRepo;
        private readonly Order _order;

        public BuyerController(IBuyerRepo orderRepo, Order order, ApplicationDbContext context)
        {
            _context = context;
            _buyerRepo = orderRepo;
            _order = order;
        }

        public IActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(Buyer buyer)
        {
            var items = _order.GetOrderItems();
            _order.OrderItems = items;
            
            if (_order.OrderItems.Count == 0)
            {
                ModelState.AddModelError("", "You do not have any selected item");
            }

            if (ModelState.IsValid)
            {
                _context.Buyers.Add(buyer);
                _context.SaveChanges();
                _order.ClearCart();
                return View("Ordered");
            }

            return View(buyer);
        }

        public IActionResult Ordered()
        {
            ViewBag.OrderedMessage = "You order is now being Procesed!";
            return View();
        }
    }

}