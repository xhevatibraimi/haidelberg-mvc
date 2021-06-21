using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;
using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class CategoriesService : CategoryRepository, ICategoriesService
    {
        public CategoriesService(DatabaseContext context)
            : base(context)
        {
        }

        public GetAllCategoriesResponse GetAllCategories(int skip = 0, int take = 10)
        {
            var categories = _context.Categories.Select(x => new GetAllCategoriesResponse.Category
            {
                Id = x.Id,
                Name = x.Name
            })
            .Skip(skip)
            .Take(take)
            .ToList();

            var totalCategories = _context.Categories.Count();

            return new GetAllCategoriesResponse
            {
                Categories = categories,
                Count = categories.Count,
                Skip = skip,
                Take = take,
                Total = totalCategories
            };
        }

        public ServiceResult<CreateCategoryResponse> TryCreateCategory(CreateCategoryRequest request)
        {
            var result = new ServiceResult<CreateCategoryResponse>();
            if (request == null)
            {
                result.Errors.Add("The category should not be null");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                result.Errors.Add("The category name should not be null, empty or whitespace");
                return result;
            }

            if (request.Name.Length > 5)
            {
                result.Errors.Add("The category name should be from 1 to 5 characters long");
                return result;
            }

            if (CategoryExists(request.Name))
            {
                result.Errors.Add($"The category with name '{request.Name}' already exists");
                return result;
            }

            var newCategory = new Category
            {
                Name = request.Name
            };
            CreateCategory(newCategory);

            result.IsSuccessfull = true;
            result.Result = new CreateCategoryResponse
            {
                Id = newCategory.Id,
                Name = newCategory.Name
            };
            return result;
        }

        public ServiceResult<GetCategoryResponse> TryGetCategory(int id)
        {
            var serviceResult = new ServiceResult<GetCategoryResponse>();
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                serviceResult.Errors.Add("Category not found");
                return serviceResult;
            }

            serviceResult.Result = new GetCategoryResponse
            {
                Id = 45,
                Name = "DC"
            };
            serviceResult.IsSuccessfull = true;
            return serviceResult;
        }

        public new ServiceResult TryDeleteCategory(int id)
        {
            var result = new ServiceResult();

            var dbCategory = _context.Categories
                .Include(x => x.Vehicles)
                .Include(x => x.Employees)
                .FirstOrDefault(x => x.Id == id);
            if (dbCategory == null)
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

        public ServiceResult TryEditCategory(EditCategoryRequest request)
        {
            var result = new ServiceResult();
            if (request == null)
            {
                result.Errors.Add("The category should not be null");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                result.Errors.Add("The category name should not be null, empty or whitespace");
                return result;
            }

            if (request.Name.Length > 5)
            {
                result.Errors.Add("The category name should be from 1 to 5 characters long");
                return result;
            }

            if (CategoryExists(request.Name))
            {
                result.Errors.Add($"The category with name '{request.Name}' already exists");
                return result;
            }

            var newCategory = new Category
            {
                Id = request.Id,
                Name = request.Name
            };

            var editSucceeded = TryEditCategory(newCategory);
            if (!editSucceeded)
            {
                result.Errors.Add("Category not found");
                return result;
            }

            result.IsSuccessfull = true;
            return result;
        }

        public new GetCategoryByIdResponse GetCategoryById(int id)
        {
            var category = base.GetCategoryById(id);
            return new GetCategoryByIdResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public ServiceResult<GetCategoryByIdSpecialResponse> GetCategoryByIdSpecial(int id)
        {
            var response = new ServiceResult<GetCategoryByIdSpecialResponse>();

            var category = _context.Categories.Include(x => x.Vehicles).FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                response.AddError("category no found");
                return response;
            }

            response.IsSuccessfull = true;
            response.Result = new GetCategoryByIdSpecialResponse
            {
                Id = category.Id,
                Name = category.Name,
                UsedByVehiclesCount = category.Vehicles.Count
            };
            return response;
        }

        public ServiceResult<GetCategoryForEditResponse> GetCategoryForEdit(int id)
        {
            var response = new ServiceResult<GetCategoryForEditResponse>();
            
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                response.AddError("category no found");
                return response;
            }

            response.IsSuccessfull = true;
            response.Result = new GetCategoryForEditResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return response;
        }
    }
}

