using System;
using System.Collections.Generic;

namespace InversionOfControl
{
    public class CategoriesService : ICategoriesService
    {
        public List<string> GetAllCategories()
        {
            System.Threading.Thread.Sleep(new Random().Next(1000, 5000));
           return new List<string> { "a", "b", "c", "d", "e" };
        }
    }
}
