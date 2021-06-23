using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers.Identity
{
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public IActionResult Index()
        {
            var serviceResult = _rolesService.TryGetAllRoles();
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(serviceResult.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var serviceResult = await _rolesService.TryDeleteRole(id);
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateRoleRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequest createRoleRequest)
        {
            var serviceResult = await _rolesService.TryCreateRole(createRoleRequest);
            if (!serviceResult.IsSuccessfull)
            {
                ViewBag.Errors = serviceResult.Errors;
                return View(createRoleRequest);
            }

            return RedirectToAction("Index");
        }
    }
}
