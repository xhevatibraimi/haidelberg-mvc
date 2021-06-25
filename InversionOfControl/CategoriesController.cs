using System.Collections.Generic;
using System.Diagnostics;

namespace InversionOfControl
{
    public class CategoriesController
    {
        private readonly ILogger _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService, ILogger logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        public List<string> GetCategories()
        {
            var sw = new Stopwatch();
            sw.Start();
            var categories = _categoriesService.GetAllCategories();
            sw.Stop();
            _logger.Log($"the categories fetched successfully in {sw.ElapsedMilliseconds} millisecons");
            return categories;
        }
    }
}
