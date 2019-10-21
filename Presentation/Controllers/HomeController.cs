using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data.Interfaces;
using Presentation.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{

    public class HomeController : Controller
    {
        private readonly IBikeRepo _bikRepo;

        public HomeController(IBikeRepo bikeRepo)
        {
            _bikRepo = bikeRepo;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ViewResult Index()
        {
             var homeVMs = new HomeVM()
            {
                Bikes =  _bikRepo.Bikes
            };
            return View(homeVMs);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
