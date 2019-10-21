using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Models
{

    public class BuyerInfo
    {
        public int BuyerInfoID { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int BikeID { get; set; }
        public int BuyerID { get; set; }
        public Bike Bike { get; set; }
        public Buyer Buyer { get; set; }

    }
}
