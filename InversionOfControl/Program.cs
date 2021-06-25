using System;

namespace InversionOfControl
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            //container.RegisterDependency<ILogger, ConsoleLogger>(() => new ConsoleLogger());
            container.RegisterDependency<ILogger, FileLogger>(() => new FileLogger(@"C:\haidelberg\log.txt"));
            container.RegisterDependency<ICategoriesService, CategoriesService>(() => new CategoriesService());
            container.RegisterDependency<CategoriesController, CategoriesController>(() =>
            {
                var logger = container.GetImplementation<ILogger>();
                var categoriesService = container.GetImplementation<ICategoriesService>();
                return new CategoriesController(categoriesService, logger);
            });

            //var logger = new FileLogger(@"C:\haidelberg\log.txt");
            //var categoriesService = new CategoriesService();
            //var categoriesController = new CategoriesController(categoriesService, logger);

            var categoriesController = container.GetImplementation<CategoriesController>();

            foreach (var category in categoriesController.GetCategories())
            {
                Console.WriteLine(category);
            }
        }
    }
}
