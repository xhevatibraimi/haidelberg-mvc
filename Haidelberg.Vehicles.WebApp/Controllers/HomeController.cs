using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService _service;
        private readonly IService _service2;

        public HomeController(IService service, IService service2)
        {
            _service = service;
            _service2 = service2;
        }

        [HttpGet("/ping")]
        public IActionResult Ping()
        {
            return Ok(_service.Counter);
        }

        public IActionResult Index()
        {
            var context = HttpContext;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/profile")]
        public IActionResult MyProfile()
        {
            //GET query route header
            //POST query route header body
            //PUT query route header body
            //DELETE query route header
            //PATCH query route header body

            // do some logic
            return View();
        }
    }
}
