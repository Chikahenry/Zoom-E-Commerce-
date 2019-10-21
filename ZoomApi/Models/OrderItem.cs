using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Models
{

    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public Bike Bike { get; set; }
        public int Amount { get; set; }
        public string OrderID { get; set; }
    }
}
