using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EasyToDo.Controls
{
    public enum EaysButtonType : byte {
        Add,
        Menu,
        Close,
        Delete,
        Back,
        Folder,
    }

    class EasyButton : Button
    {
        [DefaultValue(EaysButtonType.Add)]
        public EaysButtonType ButtonType { get; set; }

        protected override void OnCreateControl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.Selectable | ControlStyles.CacheText
                | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;
            AutoSize = false;
            FlatStyle = FlatStyle.Flat;

            BackColor = Color.FromArgb(0x22, 0x22, 0x22);

            switch (ButtonType)
            {
                case EaysButtonType.Delete:
                    BackColor = Color.FromArgb(0x33, 0x33, 0x33);
                    Size = new Size(25, 25);
                    break;

                case EaysButtonType.Folder:
                    Size = new Size(25, 25);
                    break;

                default:
                    Size = new Size(32, 32);
                    break;
            }

            base.OnCreateControl();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Cursor = Cursors.Hand;

            switch (ButtonType)
            {
                case EaysButtonType.Close:
                    BackColor = Color.FromArgb(0x99, 0x00, 0x00);
                    break;

                case EaysButtonType.Delete:
                    BackColor = Color.FromArgb(0x44, 0x44, 0x44);
                    break;

                default:
                    BackColor = Color.FromArgb(0x33, 0x33, 0x33);
                    break;
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;

            switch (ButtonType)
            {
                case EaysButtonType.Delete:
                    BackColor = Color.FromArgb(0x33, 0x33, 0x33);
                    break;

                default:
                    BackColor = Color.FromArgb(0x22, 0x22, 0x22);
                    break;
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (ButtonType)
            {
                case EaysButtonType.Close:
                    BackColor = Color.FromArgb(0xCC, 0x00, 0x00);
                    break;

                case EaysButtonType.Delete:
                    BackColor = Color.FromArgb(0x66, 0x66, 0x66);
                    break;

                default:
                    BackColor = Color.FromArgb(0x55, 0x55, 0x55);
                    break;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (ButtonType)
            {
                case EaysButtonType.Close:
                    BackColor = Color.FromArgb(0x99, 0x00, 0x00);
                    break;

                case EaysButtonType.Delete:
                    BackColor = Color.FromArgb(0x44, 0x44, 0x44);
                    break;

                default:
                    BackColor = Color.FromArgb(0x33, 0x33, 0x33);
                    break;
            }

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.Clear(Parent.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            var brush = new SolidBrush(BackColor);
            var rectangle = new Rectangle(0, 0, Size.Width, Size.Height);

            g.FillRectangle(brush, rectangle);

            switch (ButtonType)
            {
                case EaysButtonType.Add:
                    g.DrawLine(Pens.Gainsboro, 16, 10, 16, 22);
                    g.DrawLine(Pens.Gainsboro, 10, 16, 22, 16);
                    break;

                case EaysButtonType.Back:
                    g.DrawLine(Pens.Gainsboro, 10, 16, 22, 16);
                    g.DrawLine(Pens.Gainsboro, 10, 16, 15, 11);
                    g.DrawLine(Pens.Gainsboro, 10, 16, 15, 21);
                    break;

                case EaysButtonType.Menu:
                    g.DrawEllipse(Pens.Silver, 8, 15, 3, 3);
                    g.DrawEllipse(Pens.Silver, 14, 15, 3, 3);
                    g.DrawEllipse(Pens.Silver, 20, 15, 3, 3);
                    break;

                case EaysButtonType.Close:
                    g.DrawLine(Pens.Gainsboro, 11, 11, 21, 21);
                    g.DrawLine(Pens.Gainsboro, 11, 21, 21, 11);
                    break;

                case EaysButtonType.Delete:
                    g.DrawRectangle(Pens.DarkGray, 9, 4, 7, 3);
                    g.DrawLine(Pens.DarkGray, 5, 7, 20, 7);
                    g.DrawRectangle(Pens.DarkGray, 7, 7, 11, 13);
                    g.DrawLine(Pens.DarkGray, 11, 10, 11, 17);
                    g.DrawLine(Pens.DarkGray, 14, 10, 14, 17);
                    break;

                case EaysButtonType.Folder:
                    g.DrawEllipse(Pens.Gainsboro, 9, 20, 2, 2);
                    g.DrawEllipse(Pens.Gainsboro, 12, 20, 2, 2);
                    g.DrawEllipse(Pens.Gainsboro, 15, 20, 2, 2);
                    break;
            }

            brush.Dispose();
        }
    }
}
