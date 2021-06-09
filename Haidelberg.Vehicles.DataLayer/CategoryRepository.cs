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
    }
}
