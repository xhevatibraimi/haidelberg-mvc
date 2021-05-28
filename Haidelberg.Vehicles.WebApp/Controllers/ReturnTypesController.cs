using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class ReturnTypesController : Controller
    {
        [HttpGet("/learning/ok-object-result")]
        public IActionResult Index()
        {
            return new JsonResult(new List<object>
            {
                new
                {
                    FirstName = "xhevo",
                    Age = 26
                },
                new
                {
                    FirstName = "enis",
                    Age = 21
                }
            });
        }

        [HttpGet("/learning/file-result")]
        public IActionResult File()
        {
            var file = System.IO.File.ReadAllBytes("c:/files/img.png");
            return new FileContentResult(file, "image/png");
        }


        [HttpGet("/learning/exception")]
        public IActionResult FileNotFound()
        {
            var file = System.IO.File.ReadAllBytes("");
            return new FileContentResult(file, "image/png");
        }

        [HttpGet("/learning/not-implemented")]
        public IActionResult NotImplemented()
        {
            return base.StatusCode(501);
            throw new NotImplementedException();
        }
    }
}
