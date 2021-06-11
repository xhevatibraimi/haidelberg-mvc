using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.DataLayer
{
    public class VehicleRepository
    {
        private readonly DatabaseContext _context;

        public VehicleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Vehicle> GetAllVehicles()
        {
            var vehicles = _context.Vehicles.Include(x => x.Category).ToList();
            return vehicles;
        }

        public void CreateVehicle(Vehicle vehicle)
        {
            _context.Add(vehicle);
            _context.SaveChanges();
        }

        public Vehicle GetById(int id)
        {
            var vehicle = _context.Vehicles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            return vehicle;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);

            dbVehicle.CategoryId = vehicle.CategoryId;
            dbVehicle.LastRegistrationDate = vehicle.LastRegistrationDate;
            dbVehicle.LicencePlate = vehicle.LicencePlate;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.ProductionYear = vehicle.ProductionYear;

            _context.SaveChanges();
        }

        public void DeleteVehicle(int id)
        {
            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);
            if(dbVehicle != null)
            {
                _context.Remove(dbVehicle);
                _context.SaveChanges();
            }
        }
    }
}
