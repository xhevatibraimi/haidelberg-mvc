using Haidelberg.Vehicles.DataAccess.EF;
using System.Collections.Generic;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions
{
    public interface ICategoriesService
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        ServiceContentResult<Category> TryGetCategory(int id);
        ServiceResult TryCreateCategory(Category category);
        ServiceResult TryEditCategory(Category category);
        ServiceResult TryDeleteCategory(int id);
        bool CategoryExists(string name);
        bool CategoryExists(int id);
    }
}
