using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;
using Haidelberg.Vehicles.DataAccess.EF;
using Haidelberg.Vehicles.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class CategoriesService : CategoryRepository, ICategoriesService
    {
        private readonly ILogger<CategoriesService> _logger;
        public CategoriesService(DatabaseContext context, ILogger<CategoriesService> logger)
            : base(context)
        {
            _logger = logger;
        }

        public ServiceResult<GetAllCategoriesResponse> TryGetAllCategories(int skip = 0, int take = 10)
        {
            var response = new ServiceResult<GetAllCategoriesResponse>();
            try
            {
                List<GetAllCategoriesResponse.Category> categories = GetCategories(skip, take);
                int totalCategories = GetCategoriesCount();
                response.IsSuccessfull = true;
                response.Result = new GetAllCategoriesResponse
                {
                    Categories = categories,
                    Count = categories.Count,
                    Skip = skip,
                    Take = take,
                    Total = totalCategories
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error happened while fetching categories count");
                response.IsSuccessfull = false;
                response.AddError("Server Error");
                return response;
            }
        }

        private int GetCategoriesCount()
        {
            try
            {
                var sw = new Stopwatch();
                _logger.LogInformation("Started fetching categories count from database");
                sw.Start();
                var count = _context.Categories.Count();
                throw new Exception("sql connection timeout");
                sw.Stop();
                _logger.LogInformation($"Finished fetching categories count from database in {sw.ElapsedMilliseconds}");
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching categories count from database");
                throw;
            }
        }

        private List<GetAllCategoriesResponse.Category> GetCategories(int skip, int take)
        {
            try
            {
                var sw = new Stopwatch();
                _logger.LogInformation("Started fetching categories from database");
                sw.Start();

                var categories = _context.Categories.Select(x => new GetAllCategoriesResponse.Category
                {
                    Id = x.Id,
                    Name = x.Name
                })
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                sw.Stop();
                _logger.LogInformation($"Finished fetching categories from database in {sw.ElapsedMilliseconds}");
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "erorr while fetching categories from database");
                throw;
            }
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

        public ServiceResult<GetCategoryByIdResponse> TryGetCategoryById(int id)
        {
            var response = new ServiceResult<GetCategoryByIdResponse>();
            var category = base.GetCategoryById(id);

            response.IsSuccessfull = true;
            response.Result = new GetCategoryByIdResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return response;
        }

        public ServiceResult<GetCategoryByIdSpecialResponse> TryGetCategoryByIdSpecial(int id)
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

        public ServiceResult<GetCategoryForEditResponse> TryGetCategoryForEdit(int id)
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

        public ServiceResult<TryGetCategoryForDeleteResponse> TryGetCategoryForDelete(int id)
        {
            var response = new ServiceResult<TryGetCategoryForDeleteResponse>();

            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                response.AddError("category no found");
                return response;
            }

            response.IsSuccessfull = true;
            response.Result = new TryGetCategoryForDeleteResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return response;
        }

        private bool CategoryExists(string name)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return category != null;
        }
    }
}

