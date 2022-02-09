using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EasyToDo.Controls
{
    class EasyCheckBox : CheckBox
    {
        /// <summary>
        /// Rises when mouse pointer gets over the control.
        /// </summary>
        /// <param name="e">event arguments</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Rises when mouse pointer leaves the control.
        /// </summary>
        /// <param name="e">event arguments</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// rised when the control element will be created
        /// </summary>
        protected override void OnCreateControl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.Selectable | ControlStyles.CacheText
                | ControlStyles.SupportsTransparentBackColor, true);
            this.DoubleBuffered = true;

            base.OnCreateControl();
            Size s = TextRenderer.MeasureText(this.Text, this.Font);
            this.Size = new Size(this.Font.Height + s.Width + 3, s.Height + 2);
        }

        /// <summary>
        /// rised when the control element will be painted
        /// </summary>
        /// <param name="e">event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.AutoSize = false;

            int dimension = 14;
            int top = (Font.Height - 14) / 2;

            RectangleF rectangle = new RectangleF(0, top, dimension, dimension);

            // set antialias as smoothing mode
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // clear control
            e.Graphics.Clear(this.Parent.BackColor);

            // draw checkbox border
            var checkbox = Helper.DrawRoundedRectangle(rectangle, 1);
            var innerCheckbox = Helper.DrawRoundedRectangle(
                rectangle.X + 1,
                rectangle.Y + 1,
                rectangle.Width - 2,
                rectangle.Height - 2,
                1
            );
            var uncheckedFill = new SolidBrush(Parent.BackColor);

            // checkbox check state
            switch (this.CheckState)
            {
                case CheckState.Unchecked:
                    e.Graphics.FillPath(
                        Brushes.Gray,
                        checkbox
                    );
                    e.Graphics.DrawPath(
                        Pens.Gray,
                        checkbox
                    );
                    e.Graphics.FillPath(
                        uncheckedFill,
                        innerCheckbox
                    );
                    e.Graphics.DrawPath(
                        Pens.Gray,
                        innerCheckbox
                    );
                    break;

                case CheckState.Checked:
                    e.Graphics.FillPath(
                        Brushes.DodgerBlue,
                        checkbox
                    );
                    e.Graphics.DrawPath(
                        Pens.DodgerBlue,
                        checkbox
                    );
                    var check = new GraphicsPath();
                    check.AddLine(
                        dimension * .2f,
                        top + dimension * .4f,
                        dimension * .4f,
                        top + dimension * .7f
                    );
                    check.AddLine(
                        dimension * .4f,
                        top + dimension * .7f,
                        dimension * .8f,
                        top + dimension * .25f
                    );
                    Color markerColor = Color.White;
                    var marker = new Pen(markerColor, this.Font.Size * .2f);
                    e.Graphics.DrawPath(
                        marker,
                        check
                    );
                    marker.Dispose();
                    check.Dispose();
                    break;
            }

            // control text
            var foreColor = new SolidBrush(this.ForeColor);
            e.Graphics.DrawString(this.Text, this.Font, foreColor, dimension + 3, 0);

            foreColor.Dispose();
            uncheckedFill.Dispose();
            checkbox.Dispose();
        }
    }
}
