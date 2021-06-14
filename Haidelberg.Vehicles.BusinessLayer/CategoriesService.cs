using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class CategoriesService : CategoryRepository, ICategoriesService
    {
        public CategoriesService(DatabaseContext context)
            : base(context)
        {
        }

        public ServiceResult TryCreateCategory(Category category)
        {
            var result = new ServiceResult();
            if (category == null)
            {
                result.Errors.Add("The category should not be null");
                return result;
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                result.Errors.Add("The category name should not be null, empty or whitespace");
                return result;
            }

            if (category.Name.Length > 5)
            {
                result.Errors.Add("The category name should be from 1 to 5 characters long");
                return result;
            }

            if (CategoryExists(category.Name))
            {
                result.Errors.Add($"The category with name '{category.Name}' already exists");
                return result;
            }

            CreateCategory(category);

            result.IsSuccessfull = true;
            return result;
        }

        public ServiceContentResult<Category> TryGetCategory(int id)
        {
            var serviceResult = new ServiceContentResult<Category>();
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                serviceResult.Errors.Add("Category not found");
                return serviceResult;
            }

            serviceResult.Result = category;
            serviceResult.IsSuccessfull = true;
            return serviceResult;
        }

        public new ServiceResult TryDeleteCategory(int id)
        {
            var result = new ServiceResult();
            
            var dbCategory = _context.Categories
                .Include(x=>x.Vehicles)
                .Include(x => x.Employees)
                .FirstOrDefault(x => x.Id == id);
            if(dbCategory == null)
            {
                result.AddError("Category not found");
                return result;
            }

            if (dbCategory.Vehicles.Any())
            {
                result.AddError("Category cannot be deleted, because it is being used by a vehicle");
                return result;
            }

            if (dbCategory.Employees.Any())
            {
                result.AddError("Category cannot be deleted, because it is being used by an employee");
                return result;
            }

            result.IsSuccessfull = base.TryDeleteCategory(id);
            if (!result.IsSuccessfull)
            {
                result.AddError("unable to find category");
                return result;
            }

            return result;
        }

        public bool CategoryExists(string name)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return category != null;
        }

        public bool CategoryExists(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return category != null;
        }

        public new ServiceResult TryEditCategory(Category category)
        {
            var result = new ServiceResult();
            if (category == null)
            {
                result.Errors.Add("The category should not be null");
                return result;
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                result.Errors.Add("The category name should not be null, empty or whitespace");
                return result;
            }

            if (category.Name.Length > 5)
            {
                result.Errors.Add("The category name should be from 1 to 5 characters long");
                return result;
            }

            if (CategoryExists(category.Name))
            {
                result.Errors.Add($"The category with name '{category.Name}' already exists");
                return result;
            }

            var editSucceeded = base.TryEditCategory(category);
            if (!editSucceeded)
            {
                result.Errors.Add("Category not found");
                return result;
            }

            result.IsSuccessfull = true;
            return result;
        }
    }
}
