using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Data;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Models
{
    public class Order
    {
        private readonly ApplicationDbContext _db;

        public Order(ApplicationDbContext db)
        {
            _db = db;
        }
        public string OrderID { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public static Order GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Order(context) { OrderID = cartId };
        }

        public void AddToCart(Bike bike, int amount)
        {
            var item =  _db.OrderItem.SingleOrDefault(
                        s => s.Bike.BikeID == bike.BikeID && s.OrderID == OrderID);

            if (item == null)
            {
                item = new OrderItem
                {
                    OrderID = OrderID,
                    Bike = bike,
                    Amount = 1
                };

                _db.OrderItem.Add(item);
            }
            else
            {
                item.Amount++;
            }
            _db.SaveChanges();
        }

        public int RemoveFromCart(Bike bike)
        {
            var item = _db.OrderItem.SingleOrDefault(
                        s => s.Bike.BikeID == bike.BikeID && s.OrderID == OrderID);

            var amount = 0;

            if (item != null)
            {
                if (item.Amount > 1)
                {
                    item.Amount--;
                    amount = item.Amount;
                }
                else
                {
                    _db.OrderItem.Remove(item);
                }
            }

            _db.SaveChanges();

            return amount;
        }

        public List<OrderItem> GetOrderItems()
        {
            return OrderItems ?? (OrderItems =
                       _db.OrderItem.Where(c => c.OrderID == OrderID)
                           .Include(s => s.Bike).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _db
                .OrderItem
                .Where(cart => cart.OrderID == OrderID);

            _db.OrderItem.RemoveRange(cartItems);

            _db.SaveChanges();
        }

        public decimal GetTotalOrder()
        {
            var total = _db.OrderItem.Where(c => c.OrderID == OrderID)
                .Select(c => c.Bike.Price * c.Amount).Sum();
            return total;
        }
    }

}
