using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SmartMeterConsumerManagement.Controllers.ControllerUtils
{
    public class DropdownSelectListBuilder
    {
        public SelectList GetEnumTypeSelectList<TEnum>() where TEnum : Enum
        {
            List<SelectListItem> types = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = string.Empty,
                    Value = null
                }
            };
            foreach (var item in Enum.GetValues(typeof(TEnum)))
            {
                SelectListItem listItem = new SelectListItem();
                // Making enumerator value more readable
                string textWithoutUnderscore = item.ToString().ToLower().Replace('_', ' ');
                listItem.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textWithoutUnderscore);
                listItem.Value = item.ToString();
                types.Add(listItem);
            }
            return new SelectList(types, "Value", "Text");
        }
    }
}
