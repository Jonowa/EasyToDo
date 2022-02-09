using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyToDo.Controls
{
    class Helper
    {
        public static GraphicsPath DrawRoundedRectangle(float x, float y, float width, float height, float radius)
        {
            var path = new GraphicsPath();
            path.AddLine(x + radius, y, x + width - (radius * 2), y);
            path.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            path.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            path.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            path.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.AddLine(x, y + height - (radius * 2), x, y + radius);
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Draws a rectangle with rounded corners
        /// </summary>
        /// <param name="rect">Rectangle</param>
        /// <param name="radius">Radius</param>
        /// <returns></returns>
        public static GraphicsPath DrawRoundedRectangle(RectangleF rect, float radius)
        {
            return DrawRoundedRectangle(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

    }
}
