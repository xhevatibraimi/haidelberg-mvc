using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class VehicleController : Controller
    {
        private readonly DatabaseContext _context;

        // Constructor injection
        public VehicleController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vehicles = _context.Vehicles
                .Where(x => x.LastRegistrationDate < DateTime.Now.AddYears(-1))
                .ToList();

            return View();
            //            return @"
            //<!DOCTYPE html>
            //<html lang='en'>
            //<head>
            //  <meta charset='UTF-8'>
            //  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
            //  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            //  <title>Document</title>
            //</head>
            //<body>

            //</body>
            //</html>
            //";
        }
    }
}
