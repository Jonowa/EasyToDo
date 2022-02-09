using System;
using System.Drawing;
using System.Windows.Forms;

namespace EasyToDo.Controls
{
    class EasyDatePicker : DateTimePicker
    {
        protected override void OnCreateControl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.Selectable | ControlStyles.CacheText
                | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;
            AutoSize = false;

            base.OnCreateControl();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.Clear(Parent.BackColor);

            var brush = new SolidBrush(BackColor);
            var rectangle = new Rectangle(0, 0, Size.Width, Size.Height);

            g.FillRectangle(brush, rectangle);

            var pen = new Pen(Color.FromArgb(0x99, 0x99, 0x99), 1.0f);
            int x = Size.Width - 14;
            int y = Size.Height / 2 - 2;
            g.DrawLine(pen, x, y, x + 4, y + 4);
            g.DrawLine(pen, x + 4, y + 4, x + 8, y);

            var text = new SolidBrush(CalendarForeColor);
            g.DrawString(this.Value.ToShortDateString(), this.Font, text, 3, 2);

            brush.Dispose();
            pen.Dispose();
            text.Dispose();
        }
    }
}
