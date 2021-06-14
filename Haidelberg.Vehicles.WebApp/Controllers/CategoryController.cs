using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.AspNetCore.Mvc;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //ViewBag.Categories = "a,b,c,d,e";
            //ViewData["Categories"] = "a,b,c,d,e";
            //TempData["Text"] = "hello from index";
            //return RedirectToAction("Redirect");
            var categories = _categoriesService.GetAllCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            var result = _categoriesService.TryCreateCategory(model);
            if (!result.IsSuccessfull)
            {
                ViewBag.Errors = result.Errors;
                return View(model);
            }

            return RedirectToAction("Details", new { model.Id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dbCategory = _categoriesService.GetCategoryById(id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceResult = _categoriesService.TryGetCategory(id);
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
            var serviceResult = _categoriesService.TryGetCategory(id);
            if (!serviceResult.IsSuccessfull)
            {
                return RedirectToAction("Index");
            }

            return View(serviceResult.Result);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            var serviceResult = _categoriesService.TryEditCategory(category);
            if (!serviceResult.IsSuccessfull)
            {
                ViewBag.Errors = serviceResult.Errors;
                return View(category);
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
