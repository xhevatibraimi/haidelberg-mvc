using Microsoft.AspNetCore.Mvc;
using Haidelberg.Vehicles.DataAccess.EF;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DatabaseContext _context;

        public VehiclesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _context.Vehicles.Include(x => x.Category).ToList();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(new Vehicle());
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            if (!(ModelState.IsValid && _context.Categories.Any(x => x.Id == vehicle.CategoryId)))
            {
                ViewBag.ErrorMessage = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value?.Errors?.FirstOrDefault()?.ErrorMessage;
                ViewBag.Categories = _context.Categories.ToList();
                return View(vehicle);
            }

            _context.Add(vehicle);
            _context.SaveChanges();

            return RedirectToAction("Details");
        }
    }
}
