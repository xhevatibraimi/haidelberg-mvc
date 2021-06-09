using Haidelberg.Vehicles.DataAccess.EF;
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

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
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
