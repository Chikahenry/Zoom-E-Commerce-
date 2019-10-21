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
    public class BikeController : Controller
    {
        private readonly IBikeRepo _bikeRepo;
        private readonly ICategoryRepo _category;

        public BikeController(IBikeRepo bikeRepo, ICategoryRepo categoryRepo)
        {
            _bikeRepo = bikeRepo;
            _category = categoryRepo;
        }

        public ActionResult Index(string category)
        {
            string _category = category;
            IEnumerable<Bike> bikes;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                bikes = _bikeRepo.Bikes.OrderBy(p => p.BikeID);
                currentCategory = "All bikes";
            }
            else
            {
                if (string.Equals("Power", _category, StringComparison.OrdinalIgnoreCase))
                    bikes = _bikeRepo.Bikes.Where(p => p.Category.Name.Equals("Power")).OrderBy(p => p.Name);
                else
                    bikes = _bikeRepo.Bikes.Where(p => p.Category.Name.Equals("Non-Power")).OrderBy(p => p.Name);

                currentCategory = _category;
            }

            return View(new BikeLIstVM
            {
                Bikes = bikes,
                CurrentCategory = currentCategory
            });
        }

    }
}