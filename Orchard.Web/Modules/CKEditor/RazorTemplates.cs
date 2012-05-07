using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace CKEditor
{
    public static class RazorTemplates
    {
        public static IHtmlString Loop<T>(this IEnumerable<T> items, Func<T, object> itemTemplate, Func<T,object> separatorTemplate = null)
        {
            StringBuilder builder = new StringBuilder();
            var times = items.Count();
            var count = 0;
            foreach (var item in items)
            {
                // Item
                builder.Append(itemTemplate(item));
                count++;
                // Separator
                if (separatorTemplate!=null && count < times)
                {
                    builder.Append(separatorTemplate(item));
                }
            }
            return new HtmlString(builder.ToString());
        }

    }
}