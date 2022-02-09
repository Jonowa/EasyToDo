using System;
using System.Drawing;
using System.Windows.Forms;

using EasyToDo.Controller;

namespace EasyToDo.Controls
{
    class ItemPanel : FlowLayoutPanel
    {
        private readonly ItemController controller;

        private readonly ParamContainer container = ParamContainer.GetOrCreate();

        private bool ignoreUpdate;

        private const int panelHeight = 60;

        public ItemPanel(ItemController controller) : base()
        {
            this.controller = controller;
            ignoreUpdate = false;
        }

        protected override void OnCreateControl()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.Selectable | ControlStyles.CacheText
                | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;
            AutoSize = false;
            Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;

            HorizontalScroll.Visible = false;

            base.OnCreateControl();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RefreshView();
        }


        public Panel Create(int id)
        {
            int width = this.Width - 26;
            int top = 6;

            // create panel
            var panel = new Panel {
                Name = $"{id}",
                Size = new Size(width, 125),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(0x33, 0x33, 0x33),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = false,
                BorderStyle = BorderStyle.None,
            };
            // create accent line
            var accent = new Panel {
                Name = "Accent",
                Size = new Size(width, 3),
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.Silver,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = false,
            };
            // create date
            var createdAt = new Label {
                Name = "CreatedAt",
                Size = new Size(width - 16, FontHeight),
                Location = new Point(10, top),
                Padding = new Padding(0),
                Margin = new Padding(0),
                ForeColor = Color.Gray,
                Text = DateTime.Today.ToString("d. MMM"),
                TextAlign = ContentAlignment.TopRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = false,
            };
            top += createdAt.Height + 3;
            // create content
            var content = new EasyTextBox {
                Name = "Content",
                Size = new Size(width - 16, FontHeight + 2),
                Location = new Point(8, top),
                Margin = new Padding(0),
                Multiline = true,
                MaxLength = 1024,
                BackColor = Color.FromArgb(0x2E, 0x2E, 0x2E),
                ForeColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.None,
                Font = new Font(Font.FontFamily, 9),
                TextAlign = HorizontalAlignment.Left,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = false,
                AcceptsReturn = true,
            };
            top += content.Height + 7;
            // create Remind me
            var remind = new EasyCheckBox {
                Name = "Remind",
                Text = "Remind me!",
                Size = new Size(110, 20),
                Location = new Point(10, top + 3),
                Margin = new Padding(3),
                ForeColor = Color.Gray,
                Font = new Font(Font.FontFamily, 9),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            };
            // create DatePicker
            var remindAt = new EasyDatePicker {
                Name = "RemindAt",
                Size = new Size(90, 20),
                Location = new Point(120, top + 1),
                Margin = new Padding(3),
                Format = DateTimePickerFormat.Short,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                BackColor = panel.BackColor,
                CalendarForeColor = Color.WhiteSmoke,
                CalendarMonthBackground = panel.BackColor,
                CalendarTitleBackColor = panel.BackColor,
                CalendarTitleForeColor = Color.WhiteSmoke,
                CalendarTrailingForeColor = Color.WhiteSmoke,
                Visible = false,
            };

            // create delete button
            var delete = new EasyButton {
                ButtonType = EaysButtonType.Delete,
                Text = "Delete",
                Location = new Point(width - 35, top),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
            };
            panel.Size = new Size(panel.Width, top + 30);

            content.TextChanged += AdjustPanelHeight;

            content.LostFocus += EditContent;
            remind.CheckedChanged += EditRemind;
            remindAt.ValueChanged += EditRemindAt;

            delete.Click += RemoveItem;

            panel.Controls.Add(accent);
            panel.Controls.Add(createdAt);
            panel.Controls.Add(content);
            panel.Controls.Add(remind);
            panel.Controls.Add(remindAt);
            panel.Controls.Add(delete);

            this.Controls.Add(panel);
            
            Scrollbar();
            
            return panel;
        }

        public void ListItems()
        {
            ItemController.Item item;

            ignoreUpdate = true;

            for (int id = 0; id < controller.lastId; id++)
            {
                var panel = Create(id);
                item = controller.GetItem(id);
                
                var date = (Label)panel.Controls["CreatedAt"];
                date.Text = item.CreatedAt.ToString("d. MMM");

                var content = (TextBox)panel.Controls["Content"];
                content.Lines = item.Content.Split('\n');

                var remind = (CheckBox)panel.Controls["Remind"];
                remind.Checked = item.Remind;

                var remindAt = (EasyDatePicker)panel.Controls["RemindAt"];
                remindAt.Value = item.RemindAt;

                if (item.Remind)
                {
                    remindAt.Visible = true;
                    if (item.RemindAt.Date > DateTime.Today)
                    {
                        SetColor(id, Color.LimeGreen);
                    }
                    else if (item.RemindAt.Date < DateTime.Today)
                    {
                        SetColor(id, Color.Tomato);
                    }
                    else
                    {
                        SetColor(id, Color.Gold);
                    }
                }
                else
                {
                    remindAt.Visible = false;
                    SetColor(id, Color.Silver);
                }
            }

            ignoreUpdate = false;
        }

        private void AdjustPanelHeight(object obj, EventArgs e)
        {
            RefreshView();
        }

        private void EditContent(object obj, EventArgs e)
        {
            if (ignoreUpdate) return;
            var control = ((TextBox)obj).Parent;
            StoreItem(control);
        }

        private void EditRemind(object obj, EventArgs e)
        {
            if (ignoreUpdate) return;
            var control = ((CheckBox)obj).Parent;
            StoreItem(control);
        }

        private void EditRemindAt(object obj, EventArgs e)
        {
            if (ignoreUpdate) return;
            var control = ((EasyDatePicker)obj).Parent;
            StoreItem(control);
        }

        private void StoreItem(Control control)
        {
            int id = Convert.ToInt32(control.Name);
            var content = (TextBox)control.Controls["Content"];
            var remind = (CheckBox)control.Controls["Remind"];
            var remindAt = (EasyDatePicker)control.Controls["RemindAt"];

            controller.StoreItem(id, content.Text, remind.Checked, remindAt.Value.Date);
            
            if (remind.Checked)
            {
                remindAt.Visible = true;
                if (remindAt.Value.Date > DateTime.Today)
                {
                    SetColor(id, Color.LimeGreen);
                }
                else if (remindAt.Value.Date < DateTime.Today)
                {
                    SetColor(id, Color.Tomato);
                }
                else 
                { 
                    SetColor(id, Color.Gold); 
                }
            }
            else
            {
                remindAt.Visible = false;
                SetColor(id, Color.Silver);
            }
        }

        private void RemoveItem(object obj, EventArgs e)
        {
            if (Convert.ToBoolean(container.Get("program", "confirmDeletion")))
            {
                if (MessageBox.Show(
                        "Delete this entry?",
                        "EasyToDo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    ) != DialogResult.Yes) return;
            }
            
            var control = ((Button)obj).Parent;
            int id = Convert.ToInt32(control.Name);

            controller.RemoveItem(id);

            control.Dispose();
        }

        private void RefreshView()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Panel))
                {
                    EasyTextBox content = (EasyTextBox)control.Controls["Content"];
                    content.Height = (FontHeight + 2) * content.LineCount;
                    control.Size = new Size(this.Width - 26, content.Height + panelHeight);
                }
            }
        }

        private void SetColor(int id, Color color)
        {
            Panel item = (Panel)this.Controls[id.ToString()];

            Panel accent = (Panel)item.Controls["Accent"];
            accent.BackColor = color;

            EasyDatePicker remindAt = (EasyDatePicker)item.Controls["RemindAt"];
            remindAt.CalendarForeColor = color;
            remindAt.Invalidate();
        }

        private void Scrollbar()
        {
            if (this.DisplayRectangle.Height > this.Height)
            {
                VerticalScroll.Visible = false;
            }
        }
    }
}
