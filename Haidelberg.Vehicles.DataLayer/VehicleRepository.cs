using Haidelberg.Vehicles.DataAccess.EF;
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

        public void DeleteVehicle(int id)
        {
            var dbVehicle = _context.Vehicles.FirstOrDefault(x => x.Id == id);
            if (dbVehicle != null)
            {
                _context.Remove(dbVehicle);
                _context.SaveChanges();
            }
        }
    }
}
