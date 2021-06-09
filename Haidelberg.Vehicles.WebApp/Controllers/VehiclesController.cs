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
            return View(new Vehicle { LastRegistrationDate = System.DateTime.UtcNow });
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dbVehicle = _context.Vehicles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbVehicle);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(dbVehicle);
        }

        [HttpPost]
        public IActionResult Edit(int id, Vehicle vehicle)
        {
            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);

            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            if (!(ModelState.IsValid && _context.Categories.Any(x => x.Id == vehicle.CategoryId)))
            {
                ViewBag.ErrorMessages = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                ViewBag.Categories = _context.Categories.ToList();
                return View(vehicle);
            }

            dbVehicle.CategoryId = vehicle.CategoryId;
            dbVehicle.LastRegistrationDate = vehicle.LastRegistrationDate;
            dbVehicle.LicencePlate = vehicle.LicencePlate;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.ProductionYear = vehicle.ProductionYear;

            _context.SaveChanges();

            return RedirectToAction("Details", new { Id = vehicle.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dbVehicle = _context.Vehicles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbVehicle);
        }

        [HttpPost]
        public IActionResult Delete(int id, Vehicle vehicle)
        {
            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            _context.Remove(dbVehicle);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
