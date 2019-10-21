using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class BikeLIstVM
    {
        public IEnumerable<Bike> Bikes { get; set; }
        public string CurrentCategory { get; set; }

    }
}
