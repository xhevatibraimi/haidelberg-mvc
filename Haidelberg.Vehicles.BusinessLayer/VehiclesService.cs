using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class VehiclesService : VehicleRepository
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly CategoryRepository _categoryRepository;

        public VehiclesService(VehicleRepository vehicleRepository, CategoryRepository categoryRepository, DatabaseContext context)
            : base(context)
        {
            _vehicleRepository = vehicleRepository;
            _categoryRepository = categoryRepository;
        }

        public List<Vehicle> GetAllVehiclesWithCategory()
        {
            var vehicles = _context.Vehicles.Include(x => x.Category).ToList();
            return vehicles;
        }

        public List<Category> GetAllVehicleCategoriesForCreate()
        {
            var categories = _categoryRepository.GetAllCategories().OrderBy(x => x.Name).ToList();
            return categories;
        }

        public bool CategoryExistsForCreate(int categoryId)
        {
            return _context.Categories.Any(x => x.Id == categoryId);
        }

        public CreateVehicleResult CreateVehicle(Vehicle vehicle)
        {
            if (!CategoryExistsForCreate(vehicle.CategoryId))
            {
                return new CreateVehicleResult { IsSuccessfull = false, ErrorMessage = "The provided category does not exist" };
            }

            _context.Add(vehicle);
            _context.SaveChanges();
            return new CreateVehicleResult { IsSuccessfull = true };
        }

        public Vehicle GetVehicleWithCategory(int id)
        {
            var vehicle = _context.Vehicles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            return vehicle;
        }

        public bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(x => x.Id == id);
        }

        public UpdateVehicleResult UpdateVehicle(Vehicle vehicle)
        {
            if (!CategoryExistsForCreate(vehicle.CategoryId))
            {
                return new UpdateVehicleResult { IsSuccessfull = false, ErrorMessage = "Category does not exist" };
            }

            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);

            dbVehicle.CategoryId = vehicle.CategoryId;
            dbVehicle.LastRegistrationDate = vehicle.LastRegistrationDate;
            dbVehicle.LicencePlate = vehicle.LicencePlate;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.ProductionYear = vehicle.ProductionYear;

            _context.SaveChanges();

            return new UpdateVehicleResult { IsSuccessfull = true };
        }
    }
}
