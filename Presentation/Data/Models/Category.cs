using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public List<Bike> Bikes { get; set; }
        public string Description { get; set; }
    }
}
