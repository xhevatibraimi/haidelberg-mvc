using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions
{
    public interface ICategoriesService
    {
        GetAllCategoriesResponse GetAllCategories(int skip = 0, int take = 10);
        GetCategoryByIdResponse GetCategoryById(int id);
        ServiceResult<GetCategoryForEditResponse> GetCategoryForEdit(int id);
        ServiceResult<GetCategoryByIdSpecialResponse> GetCategoryByIdSpecial(int id);
        ServiceResult<GetCategoryResponse> TryGetCategory(int id);
        ServiceResult<CreateCategoryResponse> TryCreateCategory(CreateCategoryRequest request);
        ServiceResult TryEditCategory(EditCategoryRequest request);
        ServiceResult TryDeleteCategory(int id);
        bool CategoryExists(string name);
        bool CategoryExists(int id);
    }
}
