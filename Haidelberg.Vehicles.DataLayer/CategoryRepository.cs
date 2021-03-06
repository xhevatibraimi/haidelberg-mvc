using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.DataLayer
{
    public class CategoryRepository
    {
        protected readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        protected void CreateCategory(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return category;
        }

        public bool TryDeleteCategory(int id)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (dbCategory == null)
            {
                return false;
            }

            _context.Categories.Remove(dbCategory);
            _context.SaveChanges();
            return true;
        }

        public bool TryEditCategory(Category category)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (dbCategory == null)
            {
                return false;
            }

            dbCategory.Name = category.Name;
            _context.SaveChanges();
            return true;
        }
    }
}
