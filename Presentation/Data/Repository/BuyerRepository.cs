using Presentation.Data.Interfaces;
using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Repository
{
    public class BuyerRepository : IBuyerRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly Order _order;


        public BuyerRepository(ApplicationDbContext db, Order order)
        {
            _db = db;
            _order = order;
        }


        public void Create(Buyer buyer)
        {
            
            _db.Buyers.Add(buyer);

            var orderItems = _order.OrderItems;

            foreach (var item in orderItems)
            {
                var buyerInfo = new BuyerInfo()
                {
                    BuyerID = buyer.BuyerID,
                    BikeID = item.Bike.BikeID,
                    Amount = item.Amount,
                    Price = item.Bike.Price
                };

                _db.BuyerInfos.Add(buyerInfo);
            }

            _db.SaveChanges();
        }
    }
}
