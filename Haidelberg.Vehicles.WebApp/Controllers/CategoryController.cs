﻿using Haidelberg.Vehicles.BusinessLayer;
using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Haidelberg.Vehicles.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoriesService _categoriesService;

        public CategoryController(CategoriesService categoriesService)
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
            var dbCategory = _categoriesService.GetCategoryById(id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category c)
        {
            var deleteSucceeded = _categoriesService.TryDeleteCategory(id);
            if (deleteSucceeded)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Unable to delete category, probably it's been used by a vehicle or employee";
            var dbCategory = _categoriesService.GetCategoryById(id);
            return View(dbCategory);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dbCategory = _categoriesService.GetCategoryById(id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (!_categoriesService.CategoryExists(id))
            {
                return RedirectToAction("Index");
            }

            _categoriesService.Edit(category);

            return RedirectToAction("Details", new { Id = id });
        }

        ////public IActionResult Redirect()
        //{
        //    ViewBag.Text = TempData["Text"];
        //    return View();
        //}
    }
}
