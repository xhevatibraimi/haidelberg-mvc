using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //ViewBag.Categories = "a,b,c,d,e";
            //ViewData["Categories"] = "a,b,c,d,e";
            //TempData["Text"] = "hello from index";
            //return RedirectToAction("Redirect");
            var categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            _categoryRepository.CreateCategory(model);
            return RedirectToAction("Details", new { model.Id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dbCategory = _categoryRepository.GetCategoryById(id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dbCategory = _categoryRepository.GetCategoryById(id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category c)
        {
            var deleteSucceeded = _categoryRepository.TryDeleteCategory(id);
            if (deleteSucceeded)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Unable to delete category, probably it's been used by a vehicle or employee";
            var dbCategory = _categoryRepository.GetCategoryById(id);
            return View(dbCategory);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dbCategory = _categoryRepository.GetCategoryById(id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return RedirectToAction("Index");
            }

            _categoryRepository.Edit(category);
            
            return RedirectToAction("Details", new { Id = id });
        }

        ////public IActionResult Redirect()
        //{
        //    ViewBag.Text = TempData["Text"];
        //    return View();
        //}
    }
}
