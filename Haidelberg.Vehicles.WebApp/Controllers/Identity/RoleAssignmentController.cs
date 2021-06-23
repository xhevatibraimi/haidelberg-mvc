using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers.Identity
{
    public class RoleAssignmentController : Controller
    {
        private readonly IRolesService _rolesService;

        public RoleAssignmentController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("CreateRoleAssignment");
        }

        [HttpGet]
        public IActionResult CreateRoleAssignment()
        {
            var serviceResult = _rolesService.TryGetCreateRoleAssignmentScreenVM();
            if(!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(serviceResult.Result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAssignment(CreateRoleAssignmentRequest createRoleAssignmentRequest)
        {
            var serviceResult = await _rolesService.TryCreateRoleAssignment(createRoleAssignmentRequest);
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}
