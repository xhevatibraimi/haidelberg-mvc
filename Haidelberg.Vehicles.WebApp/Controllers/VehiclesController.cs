using Microsoft.AspNetCore.Mvc;
using Haidelberg.Vehicles.DataAccess.EF;
using System.Linq;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DatabaseContext _context;

        public VehiclesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        } 
    }
}
