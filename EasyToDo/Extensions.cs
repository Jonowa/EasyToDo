using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EasyToDo
{
    public static class Extensions
    {
        public static Rectangle StringToRectangle(this string value)
        {
            value = System.Text.RegularExpressions.Regex.Replace(value, "[^,\\d]+", "");
            var rc = new RectangleConverter();
            return (Rectangle)rc.ConvertFromString(null, new System.Globalization.CultureInfo("en-US"), value);
        }
    }
}
