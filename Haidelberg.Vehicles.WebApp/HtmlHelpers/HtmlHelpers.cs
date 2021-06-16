using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Haidelberg.Vehicles.WebApp.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static HtmlString DisplayErrors(this IHtmlHelper htmlHelper, List<string> errors)
        {
            if(errors == null)
            {
                return new HtmlString(string.Empty);
            }

            var result = string.Empty;
            foreach (var error in errors)
            {
                result += @$"<span class=""text-danger"">{error}</span><br />";
            }
            return new HtmlString(result);
        }
    }
}
