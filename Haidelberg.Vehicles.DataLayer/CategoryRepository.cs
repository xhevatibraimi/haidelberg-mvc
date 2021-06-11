using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.DataLayer
{
    public class CategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category category)
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
            var dbCategory = _context.Categories.Include(x => x.Vehicles).FirstOrDefault(x => x.Id == id);

            if (dbCategory.Vehicles.Any())
            {
                return false;
            }

            _context.Categories.Remove(dbCategory);
            _context.SaveChanges();
            return true;
        }

        public bool CategoryExists(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return category != null;
        }

        public void Edit(Category category)
        {
            var dbCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (dbCategory != null)
            {
                dbCategory.Name = category.Name;
                _context.SaveChanges();
            }
        }
    }
}
