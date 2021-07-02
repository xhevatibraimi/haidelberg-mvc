using Haidelberg.Vehicles.BusinessLayer;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessLayerTesting
{
    public class TryCreateCategoryShouldReturn
    {
        [Fact]
        public void Unsuccessfull_response_with_one_error_for_null_request()
        {
            // Arrange
            CreateCategoryRequest request = null;
            var context = Mock.Of<DatabaseContext>();
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 1);
            Assert.Equal("The category should not be null", response.Errors[0]);
        }

        [Fact]
        public void Unsuccessfull_response_with_one_error_for_null_category_name()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = null };
            var context = Mock.Of<DatabaseContext>();
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 1);
            Assert.Equal("The category name should not be null, empty or whitespace", response.Errors[0]);
        }

        [Fact]
        public void Unsuccessfull_response_with_one_error_for_empty_string_category_name()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = string.Empty };
            var context = Mock.Of<DatabaseContext>();
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 1);
            Assert.Equal("The category name should not be null, empty or whitespace", response.Errors[0]);
        }

        [Fact]
        public void Unsuccessfull_response_with_one_error_for_whitespace_string_category_name()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = " " };
            var context = Mock.Of<DatabaseContext>();
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 1);
            Assert.Equal("The category name should not be null, empty or whitespace", response.Errors[0]);
        }

        [Fact]
        public void Unsuccessfull_response_with_one_error_for_more_than_5_character_string_category_name()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "abcdef" };
            var context = Mock.Of<DatabaseContext>();
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 1);
            Assert.Equal("The category name should be from 1 to 5 characters long", response.Errors[0]);
        }

        //[Fact]
        //public void Unsuccessfull_response_with_one_error_for_4_character_string_category_name()
        //{
        //    // Arrange
        //    var request = new CreateCategoryRequest { Name = "abcd" };
        //    var context = Mock.Of<DatabaseContext>();
        //    var logger = Mock.Of<ILogger<CategoriesService>>();
        //    var service = new CategoriesService(context, logger);

        //    // Act
        //    var response = service.TryCreateCategory(request);

        //    // Assert
        //    Assert.NotNull(response);
        //    Assert.False(response.IsSuccessfull);
        //    Assert.NotNull(response.Errors);
        //    Assert.True(response.Errors.Count == 1);
        //    Assert.Equal("The category name should be from 1 to 5 characters long", response.Errors[0]);
        //}

        //[Fact]
        //public void Unsuccessfull_response_with_one_error_for_5_character_string_category_name()
        //{
        //    // Arrange
        //    var request = new CreateCategoryRequest { Name = "abcde" };
        //    var context = Mock.Of<DatabaseContext>();
        //    var logger = Mock.Of<ILogger<CategoriesService>>();
        //    var service = new CategoriesService(context, logger);

        //    // Act
        //    var response = service.TryCreateCategory(request);

        //    // Assert
        //    Assert.NotNull(response);
        //    Assert.False(response.IsSuccessfull);
        //    Assert.NotNull(response.Errors);
        //    Assert.True(response.Errors.Count == 1);
        //    Assert.Equal("The category name should be from 1 to 5 characters long", response.Errors[0]);
        //}

        [Fact]
        public void Unsuccessfull_response_with_one_error_for_existing_category_name()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "a" };
            var context = Mock.Of<DatabaseContext>();
            var testCategory = new Category
            {
                Id = 1,
                Name = "a"
            };
            var categories = new List<Category> { testCategory };
            context.Categories = DbContextMock.GetQueryableMockDbSet(categories);
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 1);
            Assert.Equal("The category with name 'a' already exists", response.Errors[0]);
        }

        [Fact]
        public void Successfull_response_without_errors_for_new_category()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "a" };
            var context = new Mock<DatabaseContext>();
            
            var categories = new List<Category> { };
            context.Object.Categories = DbContextMock.GetQueryableMockDbSet(categories);
            var logger = Mock.Of<ILogger<CategoriesService>>();
            var service = new CategoriesService(context.Object, logger);

            // Act
            var response = service.TryCreateCategory(request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccessfull);
            Assert.NotNull(response.Errors);
            Assert.True(response.Errors.Count == 0);
            Assert.NotNull(response.Result);
            Assert.NotNull(response.Result.Name);
            context.Verify(x => x.Add(It.IsAny<Category>()), Times.Once());
            context.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
