using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public decimal TotalOrder { get; set; }

    }
}
