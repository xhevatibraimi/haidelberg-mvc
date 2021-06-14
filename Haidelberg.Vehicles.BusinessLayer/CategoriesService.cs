using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class CategoriesService : CategoryRepository
    {
        public CategoriesService(DatabaseContext context)
            : base(context)
        {
        }

        public new ServiceResult TryCreateCategory(Category category)
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

            if (category.Name.Length < 1 || category.Name.Length > 5)
            {
                result.Errors.Add("The category name should be from 1 to 5 characters long");
                return result;
            }

            base.CreateCategory(category);

            result.IsSuccessfull = true;
            return result;
        }
    }
}
