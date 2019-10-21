using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Data.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    //[Authorize]
    public class BikesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public BikesController(ApplicationDbContext context,
                                IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Bikes
        public async Task<IActionResult> Index(string searchString)
        {
            var context = from c in _context.Bikes.Include(b => b.Category)
                          select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                context = _context.Bikes.Where(s => s.Name.Contains(searchString));
            }
            return View(await context.ToListAsync());
        }

        // GET: Bikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bikes
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BikeID == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        // GET: Bikes/Create
        public IActionResult Create() 
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");
            return View();
        }

        // POST: Bikes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BikeID,Name,Description,Price,ImageTB,Preferred,Stock,CategoryID")] BikeCreateVM bike)
        {
            if (bike != null)
            {
                string fileName = null;
                if(bike.ImageTB != null)
                {
                    string folder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + bike.ImageTB.FileName;
                    string filePath = Path.Combine(folder, fileName);
                   await bike.ImageTB.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }

                Bike newBike = new Bike
                {
                    Name = bike.Name,
                    ImageTB = fileName,
                    Stock = bike.Stock,
                    Description = bike.Description,
                    Price = bike.Price,
                    CategoryID = bike.CategoryID
                };
                _context.Bikes.Add(newBike);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = newBike.BikeID });
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", bike.CategoryID);
            return View(bike);
        }

        // GET: Bikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", bike.CategoryID);
            return View(bike);
        }

        // POST: Bikes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BikeID,Name,Description,Price,ImageTB,Preferred,Stock,CategoryID")] BikeCreateVM bike)
        {
            if (id != bike.BikeID)
            {
                return NotFound();
            }

            if (bike != null)
            {
                try
                {
                    string fileName = null;
                    if (bike.ImageTB != null)
                    {
                        string folder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + bike.ImageTB.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        await bike.ImageTB.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }  
                        Bike newBike = new Bike
                        {
                            Name = bike.Name,
                            ImageTB = fileName,
                            Stock = bike.Stock,
                            Description = bike.Description,
                            Price = bike.Price,
                            CategoryID = bike.CategoryID
                        };
                        _context.Bikes.Update(newBike);
                        await _context.SaveChangesAsync();
                      
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeExists(bike.BikeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", bike.CategoryID);
            return View(bike);
        }

        // GET: Bikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bikes
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BikeID == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BikeExists(int id)
        {
            return _context.Bikes.Any(e => e.BikeID == id);
        }
    }
}
