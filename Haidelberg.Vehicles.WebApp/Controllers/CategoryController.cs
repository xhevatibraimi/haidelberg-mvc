using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;
using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoriesService categoriesService, ILogger<CategoryController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //ViewBag.Categories = "a,b,c,d,e";
            //ViewData["Categories"] = "a,b,c,d,e";
            //TempData["Text"] = "hello from index";
            //return RedirectToAction("Redirect");
            try
            {
                var serviceResult = _categoriesService.TryGetAllCategories();

                if (serviceResult.IsSuccessfull)
                {
                    _logger.LogInformation("fetched successfull categories response");
                    return View(serviceResult.Result);
                }

                _logger.LogWarning("fetched unsuccessful categories response");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error happened while fetching categories response");
                throw;
            }
        }

        [HttpGet]
        public IActionResult DetailsSpecial(int id)
        {
            var serviceResult = _categoriesService.TryGetCategoryByIdSpecial(id);
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            return View(serviceResult.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCategoryResponse());
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryRequest request)
        {
            var serviceResult = _categoriesService.TryCreateCategory(request);
            if (!serviceResult.IsSuccessfull)
            {
                ViewBag.Errors = serviceResult.Errors;
                var model = new CreateCategoryResponse
                {
                    Name = request.Name
                };
                return View(model);
            }

            return RedirectToAction("Details", new { serviceResult.Result.Id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var serviceResult = _categoriesService.TryGetCategoryById(id);
            if (serviceResult == null)
            {
                return RedirectToAction("Index");
            }

            return View(serviceResult.Result);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceResult = _categoriesService.TryGetCategoryForDelete(id);
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            return View(serviceResult.Result);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category c)
        {
            var serviceResult = _categoriesService.TryDeleteCategory(id);
            if (serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Errors = serviceResult.Errors;

            var serviceContentResult = _categoriesService.TryGetCategory(id);
            if (!serviceContentResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            return View(serviceContentResult.Result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var serviceResult = _categoriesService.TryGetCategoryForEdit(id);
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            return View(serviceResult.Result);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditCategoryRequest request)
        {
            var serviceResult = _categoriesService.TryEditCategory(request);
            if (!serviceResult.IsSuccessfull)
            {
                ViewBag.Errors = serviceResult.Errors;
                var model = new GetCategoryForEditResponse
                {
                    Id = request.Id,
                    Name = request.Name
                };
                return View(model);
            }

            return RedirectToAction("Details", new { Id = id });
        }

        ////public IActionResult Redirect()
        //{
        //    ViewBag.Text = TempData["Text"];
        //    return View();
        //}
    }
}
