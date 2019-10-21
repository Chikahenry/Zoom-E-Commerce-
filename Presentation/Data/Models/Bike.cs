using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Models
{
    public class Bike
    {
        public int BikeID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageTB { get; set; }
       
        public bool Stock { get; set; }
        public int CategoryID { get; set; }
        [Required]
        public Category Category { get; set; }
    }

}
