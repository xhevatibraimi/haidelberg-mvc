using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoryController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            //ViewBag.Categories = "a,b,c,d,e";
            //ViewData["Categories"] = "a,b,c,d,e";
            //TempData["Text"] = "hello from index";
            //return RedirectToAction("Redirect");
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
            _context.Categories.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Details", new { Id = model.Id });
        }

        // /Category/Details/123
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category c)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (dbCategory != null)
            {
                _context.Categories.Remove(dbCategory);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            return View(dbCategory);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (dbCategory == null)
            {
                return RedirectToAction("Index");
            }

            dbCategory.Name = category.Name;
            _context.SaveChanges();

            return RedirectToAction("Details", new { Id = id });
        }

        //public IActionResult Redirect()
        //{
        //    ViewBag.Text = TempData["Text"];
        //    return View();
        //}
    }
}
