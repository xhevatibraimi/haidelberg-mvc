using Microsoft.AspNetCore.Mvc;
using Haidelberg.Vehicles.DataAccess.EF;
using System.Linq;
using Haidelberg.Vehicles.DataLayer;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly CategoryRepository _categoryRepository;

        public VehiclesController(VehicleRepository vehicleRepository, CategoryRepository categoryRepository)
        {
            _vehicleRepository = vehicleRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAllVehicles();
            return View(vehicles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(new Vehicle { LastRegistrationDate = System.DateTime.UtcNow });
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            if (!(ModelState.IsValid && _categoryRepository.CategoryExists(vehicle.CategoryId)))
            {
                ViewBag.ErrorMessage = ModelState.FirstOrDefault(x => x.Value.Errors.Any()).Value?.Errors?.FirstOrDefault()?.ErrorMessage;
                ViewBag.Categories = _categoryRepository.GetAllCategories();
                return View(vehicle);
            }

            _vehicleRepository.CreateVehicle(vehicle);
            
            return RedirectToAction("Details");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dbVehicle = _vehicleRepository.GetById(id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbVehicle);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dbVehicle = _vehicleRepository.GetById(id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View(dbVehicle);
        }

        [HttpPost]
        public IActionResult Edit(int id, Vehicle vehicle)
        {
            var dbVehicle = _vehicleRepository.GetById(vehicle.Id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            if (!(ModelState.IsValid &&_categoryRepository.CategoryExists(vehicle.CategoryId)))
            {
                ViewBag.ErrorMessages = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                ViewBag.Categories = _categoryRepository.GetAllCategories();
                return View(vehicle);
            }

            _vehicleRepository.UpdateVehicle(vehicle);

            return RedirectToAction("Details", new { Id = vehicle.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dbVehicle = _vehicleRepository.GetById(id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbVehicle);
        }

        [HttpPost]
        public IActionResult Delete(int id, Vehicle vehicle)
        {
            var dbVehicle = _vehicleRepository.GetById(id);
            if (dbVehicle == null)
            {
                return RedirectToAction("Index");
            }

            _vehicleRepository.DeleteVehicle(id);

            return RedirectToAction("Index");
        }
    }
}
