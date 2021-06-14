using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Haidelberg.Vehicles.WebApp.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = modelState
                ?.SelectMany(x => x.Value.Errors)
                ?.Select(x => x.ErrorMessage)
                ?.ToList() ?? new List<string>();

            return result;
        }
    }
}
