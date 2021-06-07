using Microsoft.AspNetCore.Mvc;
using Haidelberg.Vehicles.DataAccess.EF;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DatabaseContext _context;

        public VehiclesController(DatabaseContext context)
        {
            _context = context;
        }


    }
}
