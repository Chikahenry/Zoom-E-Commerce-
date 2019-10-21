using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Data;
using Presentation.Data.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRole(RoleVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityRole identityRole = new IdentityRole()
        //        {

        //            Name = model.RoleName
        //        };

        //        IdentityResult result = await _roleManager.CreateAsync(identityRole);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction(nameof(ListRoles));
        //        }

        //        foreach(IdentityError error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }
        //    return View(model);
        //}

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Admin admin)
        {
            var user = new IdentityUser();
            if (_context.Admins.Any(a => a.Email == admin.Email))
            {
                ViewBag.DuplicateMessage = "An Admin with this Email Exist";
                return View("Register", admin);
            }
            else
            {
               await _context.Admins.AddAsync(admin);
                _context.SaveChanges();
                ViewBag.SuccessMessage = "Account Successfully created";

            }
            if(!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin");
                var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            ViewBag.Message = "You have been registered as an admin";
            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var usr = _context.Admins.Single(a => a.Email == admin.Email && a.Password == admin.Password);
            if (usr != null)
            {
                ViewData["AdminID"] = usr.AdminID.ToString();
                ViewData["Email"] = usr.Email.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Message = "Input Fields must be Complete";
                ModelState.AddModelError("", "Email and Password enter does not match!!!");
            }
            return View(admin);
        }

        [Authorize]
        public IActionResult Index(Bike bike)
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", bike.CategoryID);
            return View();
        }
    }
}