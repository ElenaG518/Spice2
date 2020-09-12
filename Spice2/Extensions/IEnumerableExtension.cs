using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice2.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       //Text = item.GetPropertyValue("Name"),
                       Text = item.GetPropertyValue("Name").ToString(),
                        //Value = item.GetPropertyValue("Id"),
                       Value = item.GetType().GetProperty("Id").GetValue(item, null).ToString(),
                       Selected = item.GetType().GetProperty("Id").GetValue(item, null).ToString().Equals(selectedValue.ToString())
                   };
        }

    }
}