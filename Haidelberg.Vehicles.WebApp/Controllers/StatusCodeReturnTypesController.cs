using Microsoft.AspNetCore.Mvc;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class StatusCodeReturnTypesController : Controller
    {
        [HttpGet("/learning/bad-request")]
        public StatusCodeResult BadRequestAct()
        {
            return base.BadRequest();
        }

        [HttpGet("/learning/not-found")]
        public StatusCodeResult NotFound()
        {
            return base.NotFound();
        }

        [HttpGet("/learning/redirect")]
        public RedirectToActionResult Redirect()
        {
            return base.RedirectToAction("NotFound");
        }

        [HttpGet("/learning/unauthenticated")]
        public StatusCodeResult UnAuthorized()
        {
            return base.Unauthorized();
        }

        [HttpGet("/learning/unauthorized")]
        public ForbidResult Forbidden()
        {
            return base.Forbid();
        }
    }
}
