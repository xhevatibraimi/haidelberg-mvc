using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.DataLayer
{
    public class VehicleRepository
    {
        protected readonly DatabaseContext _context;

        public VehicleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Vehicle> GetAllVehicles()
        {
            var vehicles = _context.Vehicles.ToList();
            return vehicles;
        }

        public Vehicle GetById(int id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);
            return vehicle;
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
