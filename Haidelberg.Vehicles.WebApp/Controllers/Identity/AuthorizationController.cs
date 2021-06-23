using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haidelberg.Vehicles.WebApp.Controllers.Identity
{
    public class AuthorizationController : Controller
    {
        [Authorize(Roles = "Role1")]
        public IActionResult Role1() => View("~/Views/Authorization/Default.cshtml");

        [Authorize(Roles = "Role2")]
        public IActionResult Role2() => View("~/Views/Authorization/Default.cshtml");

        [Authorize(Roles = "Role3")]
        public IActionResult Role3() => View("~/Views/Authorization/Default.cshtml");
    }
}
