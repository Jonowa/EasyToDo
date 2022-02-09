using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using IWshRuntimeLibrary;

using EasyToDo.Controller;
using EasyToDo.Controls;

namespace EasyToDo
{
    public partial class FormMain : Form
    {
        private readonly ItemController controller;
        private readonly ItemPanel itemPanel;
        private readonly EasyButton addButton;
        private readonly EasyButton backButton;
        private readonly EasyButton menuButton;

        private Color borderColor = Color.DodgerBlue;

        private readonly ParamContainer container = ParamContainer.GetOrCreate();

        public FormMain()
        {
            InitializeComponent();

            this.MinimumSize = new Size(320, 165);

            this.BackColor = Color.FromArgb(0x22, 0x22, 0x22);
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            controller = new ItemController();

            addButton = new EasyButton {
                ButtonType = EaysButtonType.Add,
                Location = new Point(Left + 1, Top + 1),
                Text = "Add",
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
            };
            this.Controls.Add(addButton);

            backButton = new EasyButton {
                ButtonType = EaysButtonType.Back,
                Location = new Point(Left + 1, Top + 1),
                Text = "Back",
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Visible = false,
            };
            this.Controls.Add(backButton);

            menuButton = new EasyButton {
                ButtonType = EaysButtonType.Menu,
                Location = new Point(ClientSize.Width - 65, Top + 1),
                Text = "Menu",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            this.Controls.Add(menuButton);

            var closeButton = new EasyButton {
                ButtonType = EaysButtonType.Close,
                Location = new Point(ClientSize.Width - 33, Top + 1),
                Text = "Exit",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            this.Controls.Add(closeButton);

            itemPanel = new ItemPanel(controller) {
                Location = new Point(11, 36),
                Margin = new Padding(0),
                Padding = new Padding(0),
                Size = new Size(ClientSize.Width - 15, ClientSize.Height - 48),
                BackColor = this.BackColor,
                AutoScroll = true,
                AutoSize = true,
            };
            this.Controls.Add(itemPanel);

            addButton.Click += NewItem;
            backButton.Click += HideSettings;
            menuButton.Click += ShowSettings;
            closeButton.Click += Exit;

            panelSettings.BackColor = Color.FromArgb(0x22, 0x22, 0x22);
            panelSettings.Visible = false;
            panelSettings.Size = new Size(ClientSize.Width - 15, panelSettings.Height);
        }

        private const int cGrip = 12;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Grip size
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            // Caption bar
            rc = new Rectangle(0, 0, Width, cCaption);
            e.Graphics.FillRectangle(Brushes.Transparent, rc);
            // Border
            rc.Height = Height;
            ControlPaint.DrawBorder(e.Graphics, rc, borderColor, ButtonBorderStyle.Solid);
            // Title
            e.Graphics.DrawString(
                Application.ProductName, 
                new Font(Font.FontFamily, 12, FontStyle.Bold), 
                Brushes.White, 
                new PointF(50, 7)
            );
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }


        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            borderColor = Color.DodgerBlue;
            this.Invalidate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            borderColor = Color.DimGray;
            this.Invalidate();
        }


        /// <summary>
        /// App opened -> Load existing items
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string paramfile = Path.ChangeExtension(Application.ExecutablePath, "xml");

            try
            {
                container.Load(paramfile);
            }
            catch
            {
                container.Set("program", "confirmDeletion", false);
                container.Set("program", "autostart", false);
                container.Set("program", "dataStoragePath", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                container.Set("form", "bounds", Bounds);
            }

            Bounds = container.Get("form", "bounds").StringToRectangle();

            checkConfirmDeletion.Checked = Convert.ToBoolean(container.Get("program", "confirmDeletion"));
            checkAutostart.Checked = Convert.ToBoolean(container.Get("program", "autostart"));
            textBoxDataStorage.Text = container.Get("program", "dataStoragePath");

            string datafile = Path.Combine(textBoxDataStorage.Text, "EasyToDo.xml");

            controller.Load(datafile);
            itemPanel.ListItems();
        }

        private void Exit(object obj, EventArgs e)
        {
            container.Set("program", "confirmDeletion", checkConfirmDeletion.Checked);
            container.Set("program", "autostart", checkAutostart.Checked);
            container.Set("program", "dataStoragePath", textBoxDataStorage.Text);
            container.Set("form", "bounds", Bounds);
            container.Save();

            Application.Exit();
        }

        private void NewItem(object obj, EventArgs e)
        {
            int id = controller.AddItem();
            itemPanel.Create(id);
        }

        private void ShowSettings(object obj, EventArgs e)
        {
            itemPanel.Visible = false;
            addButton.Visible = false;
            menuButton.Visible = false;
            backButton.Visible = true;
            panelSettings.Visible = true;
        }

        private void HideSettings(object obj, EventArgs e)
        {
            itemPanel.Visible = true;
            addButton.Visible = true;
            menuButton.Visible = true;
            backButton.Visible = false;
            panelSettings.Visible = false;
        }

        private void SelectFolderForDataStorage(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;
                dialog.SelectedPath = System.IO.Path.GetDirectoryName(textBoxDataStorage.Text);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxDataStorage.Text = dialog.SelectedPath;
                    // set new path to config
                    container.Set("program", "dataStoragePath", dialog.SelectedPath);
                }
            }
        }

        private void CheckConfirmDeletionChanged(object sender, EventArgs e)
        {
            // set value to config
            container.Set("program", "confirmDeletion", ((EasyCheckBox)sender).Checked);
        }

        private void CheckStartupChanged(object sender, EventArgs e)
        {
            // set value to config
            bool autostart = ((EasyCheckBox)sender).Checked;
            container.Set("program", "autostart", autostart);
            SetStartup(autostart);
        }

        private void SetStartup(bool autostart)
        {
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            string shortcutLocation = Path.Combine(startupPath, Application.ProductName + ".lnk");

            if (autostart)
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
                shortcut.Description = Application.ProductName;
                shortcut.TargetPath = Path.ChangeExtension(Application.ExecutablePath, "exe");
                shortcut.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                shortcut.Save();
            }
            else
            {
                try
                {
                    System.IO.File.Delete(shortcutLocation);
                }
                catch { }
            }
        }
    }
}
