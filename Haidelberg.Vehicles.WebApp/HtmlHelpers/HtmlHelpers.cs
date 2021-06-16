using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Text;

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

            var sb = new StringBuilder();
            foreach (var error in errors)
            {
                sb.AppendLine(@$"<span class=""text-danger"">{error}</span><br />");
            }
            return new HtmlString(sb.ToString());
        }
    }
}
