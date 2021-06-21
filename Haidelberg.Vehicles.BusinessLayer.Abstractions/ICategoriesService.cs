using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions
{
    public interface ICategoriesService
    {
        ServiceResult<GetAllCategoriesResponse> TryGetAllCategories(int skip = 0, int take = 10);
        ServiceResult<GetCategoryByIdResponse> TryGetCategoryById(int id);
        ServiceResult<TryGetCategoryForDeleteResponse> TryGetCategoryForDelete(int id);
        ServiceResult<GetCategoryForEditResponse> TryGetCategoryForEdit(int id);
        ServiceResult<GetCategoryByIdSpecialResponse> TryGetCategoryByIdSpecial(int id);
        ServiceResult<GetCategoryResponse> TryGetCategory(int id);
        ServiceResult<CreateCategoryResponse> TryCreateCategory(CreateCategoryRequest request);
        ServiceResult TryEditCategory(EditCategoryRequest request);
        ServiceResult TryDeleteCategory(int id);
    }
}
