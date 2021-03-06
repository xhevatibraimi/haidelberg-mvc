using Haidelberg.Vehicles.BusinessLayer.Abstractions;
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
        private readonly CategoryRepository _categoryRepository;

        public VehiclesService(CategoryRepository categoryRepository, DatabaseContext context)
            : base(context)
        {
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

        public ServiceResult CreateVehicle(Vehicle vehicle)
        {
            if (!CategoryExistsForCreate(vehicle.CategoryId))
            {
                return new ServiceResult
                {
                    IsSuccessfull = false,
                    Errors = new List<string> { "The provided category does not exist" }
                };
            }

            _context.Add(vehicle);
            _context.SaveChanges();
            return new ServiceResult { IsSuccessfull = true };
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

        public ServiceResult UpdateVehicle(Vehicle vehicle)
        {
            if (!CategoryExistsForCreate(vehicle.CategoryId))
            {
                return new ServiceResult
                {
                    IsSuccessfull = false,
                    Errors = new List<string> { "Category does not exist" }
                };
            }

            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);

            dbVehicle.CategoryId = vehicle.CategoryId;
            dbVehicle.LastRegistrationDate = vehicle.LastRegistrationDate;
            dbVehicle.LicencePlate = vehicle.LicencePlate;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.ProductionYear = vehicle.ProductionYear;

            _context.SaveChanges();

            return new ServiceResult { IsSuccessfull = true };
        }
    }
}
