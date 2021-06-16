using System.Collections.Generic;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses
{
    public class GetAllCategoriesResponse
    {
        public List<Category> Categories { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}