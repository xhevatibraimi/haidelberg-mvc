using Microsoft.AspNetCore.Mvc;
using Haidelberg.Vehicles.DataAccess.EF;
using System.Linq;
using Haidelberg.Vehicles.DataLayer;
using Haidelberg.Vehicles.BusinessLayer;
using System.Collections.Generic;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehiclesService _vehiclesService;

        public VehiclesController(VehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _vehiclesService.GetAllVehiclesWithCategory();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _vehiclesService.GetAllVehicleCategoriesForCreate();
            return View(new Vehicle { LastRegistrationDate = System.DateTime.UtcNow });
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value?.Errors?.FirstOrDefault()?.ErrorMessage;
                ViewBag.Categories = _vehiclesService.GetAllVehicleCategoriesForCreate();
                return View(vehicle);
            }

            var createResult = _vehiclesService.CreateVehicle(vehicle);
            if (!createResult.IsSuccessfull)
            {
                ViewBag.ErrorMessage = createResult.ErrorMessage;
                ViewBag.Categories = _vehiclesService.GetAllVehicleCategoriesForCreate();
                return View(vehicle);
            }

            return RedirectToAction("Details", new { vehicle.Id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dbVehicle = _vehiclesService.GetVehicleWithCategory(id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbVehicle);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vehicleExists = _vehiclesService.VehicleExists(id);
            if (!vehicleExists)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _vehiclesService.GetAllVehicleCategoriesForCreate();
            var vehicle = _vehiclesService.GetVehicleWithCategory(id);
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult Edit(int id, Vehicle vehicle)
        {
            var vehicleExists = _vehiclesService.VehicleExists(vehicle.Id);
            if (!vehicleExists)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessages = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                ViewBag.Categories = _vehiclesService.GetAllVehicleCategoriesForCreate();
                return View(vehicle);
            }

            var updateResult = _vehiclesService.UpdateVehicle(vehicle);
            if (!updateResult.IsSuccessfull)
            {
                ViewBag.Categories = _vehiclesService.GetAllVehicleCategoriesForCreate();
                ViewBag.ErrorMessages = new List<string> { updateResult.ErrorMessage };
                return View(vehicle);
            }

            return RedirectToAction("Details", new { vehicle.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vehicle = _vehiclesService.GetVehicleWithCategory(id);
            if (vehicle == null)
            {
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpPost]
        public IActionResult Delete(int id, Vehicle vehicle)
        {
            var vehicleExists = _vehiclesService.VehicleExists(id);
            if (!vehicleExists)
            {
                return RedirectToAction("Index");
            }

            _vehiclesService.DeleteVehicle(id);

            return RedirectToAction("Index");
        }
    }
}